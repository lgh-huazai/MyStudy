## 查看本机是否已经生成SSH Key

```shell
ls ~/.ssh
```

r若看到有id_rsa或id_rsa.pub文件，则说明本机已经生成过。

## 查看id_rsa.pub文件

到以下磁盘找出该文件：

```
C:\Users\Z19080693\.ssh
```

SSH Key范例：

```
ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABgQChqQrGwwOuiXp8BoE6po1XlLdkYZO87HL5xVXIft+g7nF8HpPUh4Qs3yGpuNjSKEEKJmpgVZF4vi939tOTueo1Yvi66URSEVk2tEuwPhCn4z1ZxnT4ScPdrsObAPn8+fdXlOAE/JVwHnSppJcXS+Kiq7nqbg8JKbsNxsKBPZpH92kwiAUG9M9j8FWQD4PyKaaDXtoC5gQn6Brsjqo4SBovrCWR/nOIUcT5Qrg2sBlB8M/3q+GwBSbyIqaE1SV/o1l/ajApLBQ/j4MnaWewc87oE1saLNM6X3T0hxSqwgr5K0kb7UKSlyXZCbokefH9QUuLqEsgAPCJK5W0Thu2nyNvp9wJjqXTz8xue0V9PxeZanGpDr8lhnV1tcA3XBKGQD5s8Tbv3lP3ZRzIqx/GTzDZPdBcJIYidQBwpJb6rMUU6fAdUGKihvTAS5y0m2bXi4lEHmnn57wLrHcQngmc6hQ9KhF0UWykBse12/NQ6bkRHbskOIKX5ojxef5RilGVwHs= huazai_liang@wistron.com
```

## 生成SSH Key

在git bash执行以下命令：

```shell
ssh-keygen -t rsa -C "email@email.com"
```

然后会有两个提示，回车即可。

## 使用

SSH Key可用于GitHub或者GitLab等代码托管平台clone代码的时候使用SSH方式clone，这样就不用先https那种方式要输入密码了。