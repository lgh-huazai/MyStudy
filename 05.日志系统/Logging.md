# 基本概念

1、日志级别：Trace < Debug < Info < Warning < Error < Critical

2、日志提供者：LoggingProvider，把日志输出到哪里。控制台，文件，数据库

# 输入到控制台

Microsoft.Extensions.Logging（基础包，微软提供记录日志的套件）

Microsoft.Extensions.Logging.Console

# 输出到文本文件（运维人员更喜欢）

1、日志一般按照日期区分（天）

2、避免日志把磁盘撑爆，限制日志总个数或总大小

.NET里面没有内置的文本文件日志提供者。

第三方：Log4Net、**NLog**（推荐）

NLog.Extensions.Logging（与.net core logging更好的融合，没有另起炉灶）

根目录新建nlog.config文件（建议，windows忽略大小写，Linux对大小写敏感）

nlog.config解读：

- target，日志输出源，如console，file等
- rules，由logger每记录一条日志，是否匹配每一条规则，负责则往对应的target里面写

```c#
services.AddLogging(x => { 
    x.AddConsole().SetMinimumLevel(LogLevel.Trace);//微软记录控制台日志的服务
    x.AddNLog().SetMinimumLevel(LogLevel.Trace);//NLog记录日志的服务（不止于控制台）
});
```

3、可以根据业务需求分不同的模块记录日志

主要是修改nlog.config这个配置文件来实现具体的日志分类

# 结构化日志

便于统计某个错误的日志出现了多少次，以往的文本日志难以统计

# 集中化日志

对于小的系统，可以把日志放到本地。

但是在集群里面，需要保存到集中化的日志服务器。