using MyInterceptor2.attr;
using MyInterceptor2.attr.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.other
{
    public class InterceptSink : IMessageSink
    {
        private IMessageSink nextSink;

        public InterceptSink(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        #region IMessageSink Members

        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMethodCallMessage mcm = (msg as IMethodCallMessage);
            this.PreProcess(ref mcm);
            IMessage rtnMsg = nextSink.SyncProcessMessage(msg);
            IMethodReturnMessage mrm = (rtnMsg as IMethodReturnMessage);
            this.PostProcess(msg as IMethodCallMessage, ref mrm);
            return mrm;
        }

        public IMessageSink NextSink
        {
            get
            {
                return this.nextSink;
            }
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            IMessageCtrl rtnMsgCtrl = nextSink.AsyncProcessMessage(msg, replySink);
            return rtnMsgCtrl;
        }

        #endregion

        private void PreProcess(ref IMethodCallMessage msg)
        {
            PreProcessAttribute[] attrs
                = (PreProcessAttribute[])msg.MethodBase.GetCustomAttributes(typeof(PreProcessAttribute), true);
            for (int i = 0; i < attrs.Length; i++)
            {
                foreach (IPreProcessor processor in attrs[i].Processors)
                {
                    processor.Process(ref msg);
                }                
            }
        }

        private void PostProcess(IMethodCallMessage callMsg, ref IMethodReturnMessage rtnMsg)
        {
            PostProcessAttribute[] attrs
                = (PostProcessAttribute[])callMsg.MethodBase.GetCustomAttributes(typeof(PostProcessAttribute), true);
            for (int i = 0; i < attrs.Length; i++)
            {
                foreach (IPostProcessor processor in attrs[i].Processors)
                {
                    processor.Process(callMsg, ref rtnMsg);
                }
            }
        }
    }
}
