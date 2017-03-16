using MyInterceptor.attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor.other
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
            OnMethodBoundaryAspect[] attrs
                = (OnMethodBoundaryAspect[])msg.MethodBase.GetCustomAttributes(typeof(OnMethodBoundaryAspect), true);
            for (int i = 0; i < attrs.Length; i++)
                attrs[i].PreProcess(ref msg);
        }

        private void PostProcess(IMethodCallMessage callMsg, ref IMethodReturnMessage rtnMsg)
        {
            OnMethodBoundaryAspect[] attrs
                = (OnMethodBoundaryAspect[])callMsg.MethodBase.GetCustomAttributes(typeof(OnMethodBoundaryAspect), true);
            for (int i = 0; i < attrs.Length; i++)
                attrs[i].PostProcess(callMsg, ref rtnMsg);

        }
    }
}
