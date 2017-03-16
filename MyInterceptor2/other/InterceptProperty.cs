using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.other
{
    public class InterceptProperty : IContextProperty, IContributeObjectSink
    {
        public InterceptProperty()
            : base()
        {
        }
        #region IContextProperty Members

        public string Name
        {
            get
            {
                return "Intercept";
            }
        }

        public bool IsNewContextOK(Context newCtx)
        {
            InterceptProperty p = newCtx.GetProperty("Intercept") as InterceptProperty;
            if (p == null)
                return false;
            return true;
        }

        public void Freeze(Context newContext)
        {
        }

        #endregion

        #region IContributeObjectSink Members

        public System.Runtime.Remoting.Messaging.IMessageSink GetObjectSink(MarshalByRefObject obj, System.Runtime.Remoting.Messaging.IMessageSink nextSink)
        {
            return new InterceptSink(nextSink);
        }

        #endregion
    }
}
