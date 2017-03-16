using MyInterceptor.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor.attr
{
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
}
