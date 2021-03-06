# C# Method Interceptor

This will let you know how we can intercept the method in C# classes. 

We're using the approach of Remoting, and IMessageSink.  
I've created some file for you to be used readymade.

[InterceptSink.cs](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor/other/InterceptSink.cs) derived from [IMessageSink](https://msdn.microsoft.com/en-us/library/system.runtime.remoting.messaging.imessagesink(v=vs.110).aspx)

[InterceptProperty.cs](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor/other/InterceptProperty.cs) derived from [IContextProperty](https://msdn.microsoft.com/en-us/library/system.runtime.remoting.contexts.icontextproperty(v=vs.110).aspx) and [IContributeObjectSink](https://msdn.microsoft.com/en-us/library/system.runtime.remoting.contexts.icontributeobjectsink(v=vs.110).aspx)

and some interface, abstract class and attribute

**Log attribute** [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor/attr/Log.cs)
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

**Method Definition** [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor/MyClassToIntercept.cs)
```c#
class MyClassToIntercept : Interceptable
{    
    ..
    ..
    ..
    [Log]
    public override string GetGreetingMsg()
    {
        var result = string.Format("Hi {0}, your age is {1}", Name, Age);
        Console.WriteLine("I'm inside the method.");
        return result;
    }
}
```

**Calling the method as normal** [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor/Program.cs)
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



**using the 2nd approach**
We can have [PreProcessor](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/attr/other/PrePostInterface.cs) and [PostProcessor](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/attr/other/PrePostInterface.cs) separately 

Timer Processor - Pre and Post both [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/attr/TimerProcess.cs)

```c#
public class PropWrapper
{
    public string MethodName { get; set; }
    public DateTime StartTime { get; set; }
}
public class TimerProcess : IPreProcessor, IPostProcessor
{

    void IPreProcessor.Process(ref IMethodCallMessage msg)
    {
        var prop = new PropWrapper();
        prop.MethodName = msg.MethodName;
        prop.StartTime = DateTime.Now;

        //to preserver the value from Pre to Post processes, lets add this as property to Msg
        msg.Properties.Add("ExtraProperties", prop);

        Console.WriteLine("Timer started at {0}", DateTime.Now);
    }

    void IPostProcessor.Process(IMethodCallMessage callMsg, ref IMethodReturnMessage retMsg)
    {
        PropWrapper prop = callMsg.Properties["ExtraProperties"] as PropWrapper;
        TimeSpan ts = DateTime.Now.Subtract(prop.StartTime);
        Console.WriteLine("Total time for {0}:{1}ms", prop.MethodName, ts.TotalMilliseconds);
    }

}
```
Trace - Pre Processor [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/attr/TracePreProcess.cs)
```c#
public class TracePreProcess : IPreProcessor
{
    public void Process(ref IMethodCallMessage msg)
    {
        Console.WriteLine("PreProcessing:{0}", msg.MethodName);
    }
}
```

Trcee - Post Processor [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/attr/TracePostProcess.cs)
```c#
public class TracePostProcess : IPostProcessor
{
    public void Process(IMethodCallMessage callMsg, ref IMethodReturnMessage retMsg)
    {
        Console.WriteLine("Return:{0}", retMsg.ReturnValue);
    }
}
```

**Method Definition** [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/MyClassToIntercept.cs)
```c#
class MyClassToIntercept : Interceptable
{    
    ..
    ..
    ..
    [PreProcess(typeof(TimerProcess), typeof(TracePreProcess))]        
    [PostProcess(typeof(TimerProcess), typeof(TracePostProcess))] 
    public override string GetGreetingMsg()
    {
        var result = string.Format("Hi {0}, your age is {1}", Name, Age);
        Console.WriteLine("I'm inside the method.");
        return result;
    }
}
```

**Calling the method as normal** [link](https://github.com/stdeepak22/CSharp_Method_Interceptor/blob/master/MyInterceptor2/Program.cs)
```c#
var obj = new MyClassToIntercept();
obj.GetGreetingMsg();
```


**Expected result should be like -**
```
Timer started at 3/16/2017 5:26:25 PM
PreProcessing:GetGreetingMsg
I'm inside the method.
Total time for GetGreetingMsg:12.012ms
Return:Hi Deepak, your age is 26
```
