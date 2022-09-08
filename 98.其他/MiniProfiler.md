## 介绍

一款简单，功能强大的轻量级**性能分析工具**。可以帮我们定位**API响应慢**的问题。

官网：`http://miniprofiler.com`

## 使用

**1、安装Nuget包**

```shell
dotnet add package MiniProfiler.AspNetCore.Mvc
dotnet add package MiniProfiler.EntityFrameworkCore
```

![](..\99.截图\12.png)

**2、注入服务**

```c#
public void ConfigureServices(IServiceCollection services)
{
    // 其他配置...

    services.AddMiniProfiler(options =>
    {
        //访问地址路由根目录；默认为：/mini-profiler-resources
        options.RouteBasePath = "/profiler";
    })
    // 监控EntityFrameworkCore生成的SQL
    .AddEntityFramework();
}
```

**3、启用服务**

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // 必须放在UseEndpoints之前
    app.UseMiniProfiler();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

**4、Swagger UI接入Miniprofiler**

- 下载Swagger UI页面：index.html

`https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerUI/index.html`

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{

    app.UseSwaggerUI(c =>
    {
        //path to the API schema
        c.SwaggerEndpoint("swagger/v1/swagger.json", "CommonAPI v1");
        c.RoutePrefix = string.Empty;
        c.DisplayRequestDuration();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

        //read custom index.html (with MiniProfiler)
        c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("CommonAPI.wwwroot.index.html");
   });
}
```

- 将index.html添加到项目wwwroot中，并设置为嵌入资源

![](..\99.截图\14.png)

**5、获取Miniprofiler的HTML片段**

```c#
/// <summary>
/// include javascript for MiniProfiler
/// </summary>
/// <remarks>Put this in the HEAD tag of the swagger index template</remarks>
/// <returns></returns>
[HttpGet("SetupMiniProfiler")]
public IActionResult SetupMiniProfiler()
{
    var html = MiniProfiler.Current.RenderIncludes(_httpContextAccessor.HttpContext);
    return Ok(html.Value);
}
```

上面Action获取结果为：

```html
<script async="async" id="mini-profiler" src="/profiler/includes.min.js?v=4.2.22+4563a9e1ab" data-version="4.2.22+4563a9e1ab" data-path="/profiler/" data-current-id="ceb0d5c6-2571-48dd-afe4-eb7c55e64b04" data-ids="ceb0d5c6-2571-48dd-afe4-eb7c55e64b04" data-position="Left"" data-scheme="Light" data-authorized="true" data-max-traces="15" data-toggle-shortcut="Alt+P" data-trivial-milliseconds="2.0" data-ignored-duplicate-execute-types="Open,OpenAsync,Close,CloseAsync"></script>
```

讲上述结果贴到wwwroot/index.html文件的最前面。(其实不贴也可以，但是要在第二次呼叫才会有效果)

**6、运行效果**

![](..\99.截图\13.png)

