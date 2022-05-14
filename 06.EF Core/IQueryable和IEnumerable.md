# IQueryable（推荐使用）

服务器端评估：把查询操作翻译成SQL语句。

```C#
IQueryable<Book> books = ctx.Book.Where(x => x.Price > 50);
```

以上Linq操作会先翻译成SQL语句扔给数据库执行。

> IQueryable会**延迟执行**

只有在调用终结方法的时候（遍历）才会生成SQL语句执行。

当调用终结方法时才会立即执行查询，调用非终结方法时不会执行查询。

**终结方法**：ToList()，ToArray()，Min()，Max()，Count()等。

**非终结方法**：GorupBy()，OrderBy()，Skip()，Take()等。

> 如何简单判断？

返回值类型是IQueryable时，一般是非终结方法。

> 作用

可以在实际执行之前，分步构建IQueryable。特别是针对不同的ini执行不同的SQL语句时。

# IQueryable底层原理

1、ADO.NET中两个重要的对象

**DataReader**：分批从数据库中读取数据。内存占用少，DB连接时间长。

**DataTable**：把所有数据一次从数据库读取出来放到内存。内存占用大，节省DB连接。

2、IQueryable是用什么方式读取数据的呢？

采用类似于DataReader这种方式来**分批读取数据**。

如何一次性加载数据到内存？

使用ToArray()或者ToList()等。

# IEnumerable

客户端评估：即在内存中过滤数据。

```C#
IEnumerable<Book> books = ((IEnumerable<Book>)ctx.Book).Where(x => x.Price > 50);
```

以上操作先把所有数据查询出来放内存，然后在内存中过滤数据。

