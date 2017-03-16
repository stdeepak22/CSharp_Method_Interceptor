using MyInterceptor2.attr.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.attr
{
    public class TracePreProcess : IPreProcessor
    {
        public void Process(ref IMethodCallMessage msg)
        {
            Console.WriteLine("PreProcessing:{0}", msg.MethodName);
        }
    }
}
