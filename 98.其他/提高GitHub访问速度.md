## 1、获取相关的IP地址

访问`https://ipaddress.com`，查询以下两个网址对应的IP地址：

```
github.com
github.global.ssl.fastly.net
```

## 2、修改电脑的hosts文件

进入以下地址找到hosts文件：

```shell
C:\Windows\System32\drivers\etc
```

末尾处贴上以下代码：

```
140.82.114.4 github.com
199.232.69.194 github.global.ssl.fastly.net
```

## 3、刷新DNS缓存（windows）

win+r，cmd输入以下指令：

```shell
ipconfig /flushdns
```

