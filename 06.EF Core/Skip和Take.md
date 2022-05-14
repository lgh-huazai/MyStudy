1、常见分页的类型

Web：通过分页

App：通过向下滑动

2、Linq中的分页写法

传入参数有pageIndex（页码），pageSize（每页大小）

```c#
IQueryable<Book> books = ctx.Book.Where(x => x.Price > 50);
// 获取某一页的数据
var data = books.Skip((pageIndex - 1) * pageSize).Take(pageSize);
// 获取总条数
int count = books.Count();
// 获取总页数
int pageCount = (int)Math.Ceiling(count * 1.0 / pageSize);
```

> 为什么要 * 1.0？

在c#中/是取整的意思，所以要变成小数运算，这样的结果才有小数。

> Ceiling和Floor

Ceiling是取天花板的意思。

```c#
Math.Ceiling(1.1); // 2
Math.Ceiling(1.9); // 2
Math.Ceiling(-1.1); // -1
Math.Ceiling(-1.9); // -1
```

Floor是取最底下的值。

```c#
Math.Floor(1.1); // 1
Math.Floor(1.9); // 1
Math.Floor(-1.1); // -2
Math.Floor(-1.9); // -2
```

