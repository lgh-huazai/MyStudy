1、Migration原理

当代码对实体进行修改（包括新增和编辑）时，使用Add-Migration命令会在本地生成一个迁移脚本，此迁移脚本其实就是一个类，然后继承了Migration，重写了Up（向上迁移）和Dowm（向下迁移）的方法，当执行Update-Database命令后，EF Core会将这次的改变同步到数据库。

2、Migration的其他命令

- Update-Database xxx：回滚到xxx的状态，迁移脚本不变

- Remove-Migration：删除最后一次迁移的脚本

- Script-Migration：生成迁移的SQL，从开始到最新

  比如生成从A-B的SQL：Script-Migration A B

3、为什么还需要Script-Migration？

因为Add-Migration只适用于迁移到开发环境，如果更新到生产环境，则一般是提供SQL。