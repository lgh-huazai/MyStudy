## 1、URL映射传参（Get）

https://localhost/book/get/1/2

```c#
[HttpGet("{x}/{y}")]
public int Get(int x, int y)
{
    return x + y;
}
```

参数名和URL的占位符需保持一致（若不一致，需用FromRoute）。

## 2、QueryString传参（Get）

默认就是从query string传参，比如：

https://localhost/book?id=1

```c#
[HttpGet]
public int Get(int id)
{
    return id;
}
```

如果要显式从query string拿参数，且名字保持一致时，写法如下：

```c#
[HttpGet]
public int Get([FromQuery]int id)
{
    return id;
}
```

若名字不一致，写法如下：

```c#
[HttpGet]
public int Get([FromQuery(Name = "aaa")] int id)
{
    return id;
}
```

## 3、请求报文体（Post）

一般而言，只需在action方法参数定义一个class去接收即可。

此时，传过来的type一定要设置为**application/json**