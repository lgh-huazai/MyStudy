# 为什么要学Linq

让数据处理变得简单！（可以链式编程，就是一直使用扩展方法）

Linq的演变过程：**委托-->Lambda-->Linq**

1、委托

委托是方法的类型。调用委托的变量时执行的就是变量指向的方法。

```C#
static void F1(){};
delegate void D;//定义一个委托
D d = F1;//声明一个委托的变量指向F1方法
d();//执行d变量指向的方法
```

其实在开发中，很少自己定义委托类型，都是用**微软内置的委托**：

**Action**：无返回值（最多有16个参数）

**Func**：有返回值（最多有16个参数），返回值必须放在参数列表的最后一个

委托变量不仅可以指向普通方法，还可以指向匿名方法。

```c#
Func<int, int, int> d = delegate (int i1, int i2) 
{ 
    return i1 +i2;
};
d(1,2);

Func<int, int, int> d = (int i1, int i2) =>
{ 
    return i1 +i2;
};
d(1,2);

//省略参数类型
Func<int, int, int> d = (i1, i2) =>
{ 
    return i1 +i2;
};
d(1,2);
```

若方法**没有返回值**且方法体**只有一行代码**，可以省略大括号。

若只有一个参数，可以省略小括号。

```c#
Action d = () => Console.WriteLine("Huazai");
Action<string> d = s => Console.WriteLine(s);
```

2、Lambda

定义：匿名函数的简化版。

作用：使代码变得简洁

# 使用Linq的好处

Linq提供了很多处理集合的扩展方法，配合lambda能简化数据处理。

一个公司的面试题：不用Linq里面的Where方法，自己写一个（主要考察你有没有理解Linq）

```C#
// 基础版
public static IEnumerable<int> MyWhere(this IEnumerable<int> data, Func<int, bool> func)
{
    var result = new List<int>();
    foreach(int i in data)
    {
        if(func(i) == true)
        {
            data.Add(i);
        }
    }
    return result;
}
// 升级版：使用yield return，提高性能！
public static IEnumerable<int> MyWhere1(this IEnumerable<int> data, Func<int, bool> func)
{
    foreach(int i in data)
    {
        if(func(i) == true)
        {
            yield return i;
        }
    }
}

// 调用（从数组中取出大于2的数）
int[] array = new int[5]{1,2,3,4,5};
MyWhere(array, x => x > 2);
```

# var

**推断类型**，由编译器自动推断，是强类型！

与JavaScript的var不一样，它是动态类型。

若想在C#中使用像JavaScript的动态类型，用dynamic。

**var + 匿名类型**才是精髓！

```C#
var a = new { Name = "xiaoming" Age = 1 };
```

# 常用的扩展方法

什么是扩展方法？

能够向现有类型添加方法。是一种特殊的静态方法。

大部分在System.Linq命名空间里面。

由于扩展方法的返回类型都是IEnumerable<T>类型，所以可以链式调用

1、**OrderBy(), ThenBy()**

```C#
// 根据姓名的最后一个字符升序排序
data.OrderBy(x => x.Name.SubString(x.Name.Length - 1));
```

2、限制结果集，获取部分数据：

**Skip(n)**：跳过n条数据

**Take(n)**：获取n条数据

```C#
// 跳过5条数据，取10条
data.Skip(5).Take(10);
```

3、数据分组（**GroupBy**）

根据年龄分组，并统计该年龄的最大工资是多少

```c#
var items = data.GroupBy(x => x.Age);
foreach(var g in items)
{
    Console.WriteLine(g.Age);
    Console.WriteLine($"最大工资:" + g.Max(x => x.Salary));
}
```

4、**ToArray**和**ToList**

5、链式调用综合应用

```c#
var data = list.Where(x => x.Id > 2).GroupBy(x => x.Age).OrderBy(x => x.Key).Take(3).Select(x => new
                                                                                            {
    Age = x.Key, Count = x.Count(), AverageSalary = x.Average(y => y.Salary)
});
```

# Linq中两种写法

1、**方法语法**

如上所示的就是方法语法的使用。其实就是链式编程。

2、**查询语法**

使用Linq子句。这种写法更像SQL语句！

在源码阶段写法不一样，但最终编译器编译结果一样