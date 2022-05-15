## 顶级语句

直接在c#文件编写入口方法的代码，不用类，不用main。

同一个项目中只能有一个文件具有顶级语句。

## 全局using

将global修饰符添加到using前面，这个命名空间将应用到整个项目。

通常创建一个用来编写全局using的c#文件。

如果项目中启用了<ImplicitUsings>enable</ImplicitUsings>，代表编译器会隐式添加对System，System.Linq等常用的命名空间的引入。

## using资源管理（C#8）

我们知道，实现了IDisposible接口的类可以使用using来进行资源释放。

以前的做法都是try catch来进行资源的手动释放。

1、一般什么资源需要释放？

非托管资源。例如操作文件，网络请求等。

2、若有很多using嵌套呢？

传统的写法：

```c#
using(var conn = new SqlConnection())
{
    using(var cmd = new conn.CreateCommand())
    {
        using(var reader = new cmd.ExcuteReader())
        {
            ......
        }
    }
}
```

上面的操作会显示的特别臃肿和麻烦。因此有以下优化：

```C#
using var conn = new SqlConnection();
using var cmd = new conn.CreateCommand();
using var reader = new cmd.ExcuteReader();
```

那什么时候会进行资源的释放呢？

当变量离开作用域范围时，资源就被释放。比如：变量定义在函数里面，则出函数后资源被释放。

## 可空引用类型（C#8）

csproj中<Nullable>true</Nullable>启用可空引用类型检查。（默认是添加的）

如何使用：

在可空类型后面加”?“修饰符来声明这个类型是可空的。

对于没有添加”?“的修饰符的引用类型，编译器会**给出警告信息**。

可以使用!来强制取消警告：

```c#
a.Name!.ToString();
```

## record类型（C#9）

自动帮我们重写ToString()方法，打印每一个属性。

自动帮我们重写Equals()方法，让我们相同内容的对象进行比较时为true。

语法：

```c#
public record Book(int Id, string Name);
```

使用：

```c#
var book = new Book(1, "aaa");
```

原理：record其实就是c#编译器的一个语法糖，最终编译其实也是一个普通的类。