using MyInterceptor2.attr.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.attr
{
    public class TracePostProcess : IPostProcessor
    {
        public void Process(IMethodCallMessage callMsg, ref IMethodReturnMessage retMsg)
        {
            Console.WriteLine("Return:{0}", retMsg.ReturnValue);
        }
    }
}
