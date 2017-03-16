using MyInterceptor2.attr.other;
using MyInterceptor2.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.attr
{
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
}
