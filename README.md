# C# Method Interceptor

This will let you know how we can intercept the method in C# classes. 

We're using the approach of Remoting, and IMessageSink


**Log attribute**
```c#
public sealed class LogAttribute : OnMethodBoundaryAspect
{
    public override void PreProcess(ref IMethodCallMessage msg)
    {
        Console.WriteLine("PreProcessing:{0}", msg.MethodName);
    }
    public override void PostProcess(IMethodCallMessage callMsg, ref IMethodReturnMessage retMsg)
    {
        Console.WriteLine("Return:{0}", retMsg.ReturnValue);
    }
}
```

**Method Definition**
```c#
class MyClassToIntercept : Interceptable
{    
    [Log]
    public override string GetGreetingMsg()
    {
        var result = string.Format("Hi {0}, your age is {1}", Name, Age);
        Console.WriteLine("I'm inside the method.");
        return result;
    }
}
```

**Calling the method as normal**
```c#
var obj = new MyClassToIntercept();
obj.GetGreetingMsg();
```

We will get the result something, Pre processing for the method, then method execution then Post method execution.  


```
PreProcessing:GetGreetingMsg
I'm inside the method.
Return:Hi Deepak, your age is 26
```

