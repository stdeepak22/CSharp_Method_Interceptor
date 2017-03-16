using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor.attr
{
    public abstract class OnMethodBoundaryAspect : Attribute
    {
        public virtual void PreProcess(ref IMethodCallMessage msg) { }
        public virtual void PostProcess(IMethodCallMessage callMsg, ref IMethodReturnMessage retMsg) { }
    }
}
