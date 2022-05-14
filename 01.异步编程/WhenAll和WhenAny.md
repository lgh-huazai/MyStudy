# WhenAny

> 任何一个task完成，任务就完成。

# WhenAll

> 所有task完成，任务才完成。

# 注意点

1、接口里面的方法不需要写async

因为接口方法没有实现，跟async的设计违背，故报语法错误。

2、异步与yield

yield return不仅能简化数据的返回，还可以让数据处理流水线化，提高性能。

```c#
public IEnumerable<string> Test()
{
    yield return "小明";
    yield return "小红";
    yield return "小李";
}
```

在C#8.0前，async方法内不能使用yield。