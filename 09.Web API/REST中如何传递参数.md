## HTTP传递参数三种方式

1、URL（长度限制）

2、QueryString（灵活，长度限制）

3、请求报文体（灵活，长度不受限制）

## HTTP如何返回状态码

1、200派

2、400派

那该如何设计？

- 对于非业务错误（技术错误），理应返回4xx，5xx；

- 对于业务错误，返回相应的HTTP状态码，但是响应内容应该给出更详细的错误码;

```json
{
    "code":"123",
    "message":"xxxx"
}
```

## 注意

1、webapi的controller的action方法必须标注HTTP方法，避免swagger渲染的时候报错。

2、Controller（MVC）和ControllerBase（WebAPI）

3、Controller继承ControllerBase，因为要特定提供MVC一些特殊的方法。

4、控制器可以不显式继承任何基类（Controller，ControllerBase），只需要标注ApiController属性。

但是这样用不了基类的任何方法。