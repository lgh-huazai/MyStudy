# 痛点

1、当我们需要开发报表时，报表SQL语句达上百上千行；

2、EF Core Provider功能不够强大，支持语法不够SQL语法强大。

换言之就是C#代码有可能被翻译不出来SQL语句（由于每种数据库都有特殊的语法）。

# 三种写法

**1、执行非查询SQL语句**

```sql
insert into book (name, price, author, pdate)
select name, price, author, pdate from book where price > 50;
```

此写法EF Core是不支持的。

```c#
string sql = $"insert into book (name, price, publishdate) value ('计算机组成原理', 55, {DateTime.Now})";
ctx.Database.ExecuteSqlInterpolated(sql);
```

这样的写法会有SQL注入的问题吗？

上面的SQL语句看起来是通过字符串拼接的，但是经过EF Core后，会以变量的方式呈现。

```shell
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (330ms) [Parameters=[@p0='?' (DbType = DateTime)], CommandType='Text', CommandTimeout='30']
      insert into book (name, price, publishdate) value ('计算机组成原理', 55, @p0)
```

从上面可以看出，不会有SQL注入的问题。

**2、执行实体相关的查询SQL语句**

调用对应实体的DbSet的FromSqlInterpolated方法

```c#
ctx.Book.FromSqlInterpolated($"select * from book");
```

**3、执行任意查询语句**

其实就是直接使用原生的ADO.NET来实现。

```sql
var conn = myContext.Database.GetDbConnection();//拿到底层的connection对象
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }
    using (var cmd = conn.CreateCommand())
    {
        cmd.CommandText = "select * from book order by price";
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(1) + "," + reader.GetDouble(2));
            }
        }
    }
```

但是在EF Core里面写ADO.NET代码比较复杂繁琐，所以直接建议使用**Dapper**来做。

**4、使用Dapper**

```c#
string connStr = "Server=localhost;Port=3306;Database=huazai;Uid=root;Pwd=123456;Charset=utf8;";
    using (var sqlConn = new MySqlConnection(connStr))
    {
        var data = sqlConn.Query<Book>("select * from book where price > @Price", new { Price = 50 });
        foreach (var item in data)
        {
            Console.WriteLine(item.Id + "," + item.Name + "," + item.Price + "," + item.PublishDate);
        }
    }
```

