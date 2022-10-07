## 1、Session

以前的网站，由于http协议是无状态的，是怎么知道这次请求是谁呢？

用户登录后，在服务器会生成一个sessionid与这个用户信息，并把这个对应关系保存起来（存在内存中），

并把这个sessionid返回给浏览器，并存在cookie中，下一次请求的时候带上这个sessionid，这样就知道这次

请求是谁了。

1、什么是无状态请求？

也就是第一次http请求和第二次http请求没有任何关系。

2、cookie是指什么？

存储在用户终端上的数据。一般分为两种：

- 会话cookie：一般存在浏览器进程的内存中，浏览器关闭cookie就失效
- 本地磁盘cookie：存在本地磁盘中，永久有效

## Session缺点

对于分布式集群的环境，session就有点力不从心了。

但是，也有解决方案：就是部署一台中心状态服务器（Redis、Memcached），客户端的请求读到这台中心

状态服务器拿状态。

其实，现在的web系统已不太建议这种方案了。为什么？

1、性能问题（从内存读取的速度肯定快于跨服务器去中心服务器读取）

2、有的客户端保存cookie不太方便（app，小程序）

## 2、JWT（Json Web Token）

JWT把登录信息保存在客户端。

为了防止客户端数据造假，保存在客户端的令牌经过签名处理，签名的秘钥只有服务器端才知道，

**每次服务器收到客户端提交过来的令牌都要检查一下签名**。

token有三部分组成：

1、头部header（签名算法，比如SHA256）

2、负载payload（用户信息，比如username）

3、签名signature（签名算法：header + payload + 服务器才知道的秘钥）

服务器拿到token后，先用秘钥 + token里的header + token里的payload经过签名算法生成一个签名，

然后再跟客户端传过来的签名进行比对，若错误则报错，若一致则使用token里面的用户信息。

JWT优点：

1、状态保存在客户端，天然适合分布式系统

2、签名机制保证了客户端token不会数据造假

3、性能更高（纯内存字符串操作，不需要与第三方服务器通讯）

## 如何生成token

在项目中安装以下nuget包：

```
System.IdentityModel.Tokens.JWT
```

![](..\99.Images\18.png)

1、生成token的代码：

```c#
var claims = new List<Claim>(); //一个Chaim代表payload里面的一项信息
claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
claims.Add(new Claim(ClaimTypes.Name, "huazai"));
claims.Add(new Claim(ClaimTypes.Role, "User"));
claims.Add(new Claim(ClaimTypes.Role, "Admin"));
claims.Add(new Claim("PassPort", "E90000082"));
string key = "fasdfad&9045dafz222#fadpio@0232"; //仅服务器端知道的秘钥
DateTime expires = DateTime.Now.AddDays(1); //token的过期时间
byte[] secBytes = Encoding.UTF8.GetBytes(key);
var secKey = new SymmetricSecurityKey(secBytes);
var credentials = new SigningCredentials(secKey,SecurityAlgorithms.HmacSha256Signature);
var tokenDescriptor = new JwtSecurityToken(claims: claims,
    expires: expires, signingCredentials: credentials);
string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
Console.WriteLine(jwt);
```

2、解析客户端传过来的token

```c#
string jwt = Console.ReadLine()!;
string[] segments = jwt.Split('.');
string head = JwtDecode(segments[0]);
string payload = JwtDecode(segments[1]);
Console.WriteLine("--------head--------");
Console.WriteLine(head);
Console.WriteLine("--------payload--------");
Console.WriteLine(payload);
string JwtDecode(string s)
{
	s = s.Replace('-', '+').Replace('_', '/');
	switch (s.Length % 4)
	{
		case 2:
			s += "==";
			break;
		case 3:
			s += "=";
			break;
	}
	var bytes = Convert.FromBase64String(s);
	return Encoding.UTF8.GetString(bytes);
}
```

注意：

1、负载中的信息是明文保存的；

2、不要把不能被客户端知道的信息保存在token当中；

## 如何对签名进行校验

一般客户端传过来的token都具有被篡改过的可能，所以==服务器端需要对token进行校验==。

可以使用代码进行校验，后面也可以把校验的工作嵌入到swagger中。

代码校验：

```c#
string jwt = Console.ReadLine()!;
string secKey = "fasdfad&9045dafz222#fadpio@0232"; //仅服务器端知道的秘钥
JwtSecurityTokenHandler tokenHandler = new();
TokenValidationParameters valParam = new();
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
valParam.IssuerSigningKey = securityKey;
valParam.ValidateIssuer = false;
valParam.ValidateAudience = false;
// 若token被篡改过，则会在ValidateToken方法发生异常
ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt,
		valParam, out SecurityToken secToken);
foreach (var claim in claimsPrincipal.Claims)
{
	Console.WriteLine($"{claim.Type}={claim.Value}");
}
```

## ASP.NET Core对JWT的校验封装

上面讲到的都是JWT的原理，其实到真正项目中，ASP.NET Core有对JWT进行封装，并不需要自己动手写。

**以下组件主要是对JWT进行校验**。

1、配置文件里面配置JWT节点

主要是秘钥（SigninKey）和过期时间（ExpireSeconds）两个值。

2、安装nuget包

```shell
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

![](..\99.Images\22.png)

```c#
services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x =>
{
	var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
	byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt.SigningKey);
	var secKey = new SymmetricSecurityKey(keyBytes);
	x.TokenValidationParameters = new()
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true, //进行token是否过期校验
		ValidateIssuerSigningKey = true, //进行签名校验
		IssuerSigningKey = secKey
	};
});
```

3、在app.UseAuthorizaton()之前添加app.UseAuthentication()

4、编写login的代码，把生成的token返回给客户端

5、在控制器或者action中加入attritube：**[Authorize]**

代表此控制器或者action需要登录才能访问这个接口。

对于被标记了Authorized的控制器的某个操作方法不想被验证，可以加上**[AllowAnonymous]**

6、需需要在请求报文头（header）加上key-value

key：**Authorization** value：**Bearer token**（token需要填实际要传的token）

加上后才能正确的访问带有Authorized attritube的控制器或者方法。

7、如何获取token里面的payload信息呢？

```c#
string id = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
string userName = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
IEnumerable<Claim> roleClaims = this.User.FindAll(ClaimTypes.Role);
```

8、JWT在app，小程序保存在哪里？

存到全局变量或者文件里面。

## Swagger集成JWT

上面的代码中，每个请求都要把JWT塞到header里面才能请求成功，在postman里面去塞header。

那如何在swagger中就可以塞token到header了呢？

```c#
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,
            Id = "Authorization"},
        Scheme = "oauth2",Name = "Authorization",
        In = ParameterLocation.Header,Type = SecuritySchemeType.ApiKey,
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
}); 
```

效果：

![](..\99.Images\23.png)