using MyInterceptor.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor.attr
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InterceptableClassAttribute : ContextAttribute
    {
        public InterceptableClassAttribute(string name = "Intercept")
            : base(name)
        {

        }
        public override void Freeze(Context newContext)
        {
        }

        public override void GetPropertiesForNewContext(System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
        {
            ctorMsg.ContextProperties.Add(new InterceptProperty());
        }

        public override bool IsContextOK(Context ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
        {
            InterceptProperty p = ctx.GetProperty("Intercept") as InterceptProperty;
            if (p == null)
                return false;
            return true;
        }

        public override bool IsNewContextOK(Context newCtx)
        {
            InterceptProperty p = newCtx.GetProperty("Intercept") as InterceptProperty;
            if (p == null)
                return false;
            return true;
        }
    }
}
