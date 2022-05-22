在控制台程序中，我们需要手动创建ServiceCollection，IServiceProvider。

但是在WebAPI中，不需要手动创建了，框架帮我们自动创建。

比如：（.NET6 Web API创建的模板）

```c#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
```

## 依赖注入的形式

1、构造函数注入

2、参数注入（特殊情况的时候使用）

有时候一些服务在构造函数注入会很慢，使用其他action方法的时候导致整个app都卡在服务注入那一段。

这个时候，我们应该是什么时候用到才注入进来。比如

```c#
[HttpGet]
public void Test([FromService]TestService testService)
{
    return testService.Count();
}
```

