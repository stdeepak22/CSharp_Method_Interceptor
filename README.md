# C# Method Interceptor

This will let you know how we can intercept the method in C# classes. 

We're using the approach of Remoting, and IMessageSink


```
var obj = new User();
obj.CallMethod();
```

We are the result something, Pre processing for the method, then method execution then Post method execution.  


```
PreProcessing - Method = "CallMethod"
I'm inside the method.
Result - "This is the returning result from CallMethod".
```

