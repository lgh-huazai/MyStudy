面试的时候算法题一般尽量避免使用正则表达式，LINQ等这些高级类库。

最好使用for循环，if语句，数组等基本语法实现

1、统计一个字符串中每个字母出现的频率（忽略大小写），然后从高到低的顺序输出出现频率高于两次的单词和其出现的频率。

```c#
string s = "hello world, liang guo hua! hahaha";
s.Where(x => char.IsLetter(x)).Select(x => char.ToLower(x)).GroupBy(x => x).Select(x => new { x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).Where(x => x.Count > 2);
```

此方法用于检查**指定的Unicode字符**是否与**Unicode字母**匹配

即只取出字母，数字空格符号等都被过滤掉！