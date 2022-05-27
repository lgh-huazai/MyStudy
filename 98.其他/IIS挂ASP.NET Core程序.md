## 如何检查server是否安装ASP.NET Core Runtime

![](..\99.截图\19.png)

点击模块后看到以下选择则代表已安装Runtime。

![](..\99.截图\20.png)

## Server安装ASP.NET Core Runtime

访问微软官网，下载对应版本的runtime：

`https://dotnet.microsoft.com/zh-cn/download/dotnet`

有三个版本的runtime，我们要部署的是ASP.NET Core的程序，故使用以下版本：

![](..\99.截图\21.png)

下载后在server安装，再重复第一步的动作，检查是否装好。

- 新建一个用来托管asp.net core应用程序的应用池（application pool）：

![](D:\MyStudy\99.截图\22.png)

- 右键新建一个网站（portal）

```
sites -> add website
```

- 在网站下面新建application，并将这个网站所用的应用池指向上面创建的引用池：

网站右键 -> Add Application...

- 把publish的asp.net core代码copy到application所在的文件夹底下
- 访问程序地址

![](..\99.截图\23.png)