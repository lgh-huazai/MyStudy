# 概念

依赖注入（DI）是控制反转（IOC）思想的实现方式。

依赖注入简化模块的组装过程，降低模块之间的耦合度。

控制反转的**目的**：怎样创建xx对象 => 我要xx对象

控制反转的两种实现方式：

1、服务提供器（IServiceProvider）-需要做很多前置工作。比如先往ServiceCollection里添加服务，然后Build ServiceProvider，然后在使用GetService()

2、依赖注入（DI）-我需要什么服务就给我什么服务

# 依赖注入

1、服务(service)：就是你要的那个对象

比如你要一个连接数据库的服务来操作数据库

2、注册服务：

由于服务并不是凭空而来的，所以要在服务容器里面注册

3、服务容器

负责管理注册的服务

4、服务生命周期

**Transient**(瞬态)

每次从容器获取都能获取到新的对象。

**Scoped**(范围)

每次从容器获取后在某一范围内使用。

ASP.NET Core里面定义默认scoped就是一次服务器请求，但是程序员可以指定他的scoped（IServiceScope）

**Singleton**(单例)

无论谁获取这个服务，都是拿到同一个对象

# .NET中怎么使用依赖注入

建议面向接口编程，更灵活（从设计模式中知道，会有很多好处）

> 使用服务定位器

1、添加Nuget包

```cmd
Install-package Microsoft.
```

# 注意

1、判断两个对象是否为同一个对象：

```c#
object.RefrenceEquals(obj1, obj2);
```

2、如果一个类实现了IDisposable接口，离开作用域之后会自动调用对象的Dispose方法。