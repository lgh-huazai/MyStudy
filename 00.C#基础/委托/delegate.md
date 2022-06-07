## 1、定义一个委托类型

可以在**命名空间中**、**类中**或**全局命名空间中**（不建议）定义委托类型。

```c#
// Define the delegate type:
public delegate int Comparison<in T>(T left, T right);
```

## 2、声明一个该委托类型的对象

```c#
// Declare an instance of that type:
public Comparison<T> comparator;
```

## 3、为委托对象添加目标方法

添加单个目标方法到委托：

```c#
comparator = (string a, string b) => a.Length.CompareTo(b.Length);
```

## 4、调用委托

```c#
int result = comparator(left, right);
```

