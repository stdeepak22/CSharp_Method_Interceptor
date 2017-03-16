using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.attr.other
{    
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class PreProcessAttribute : Attribute
    {
        private IPreProcessor p;
        public PreProcessAttribute(Type preProcessorType)
        {
            this.p = Activator.CreateInstance(preProcessorType) as IPreProcessor;
            if (this.p == null)
                throw new ArgumentException(String.Format("The type '{0}' does not implement interface IPreProcessor", preProcessorType.Name, "processorType"));
        }

        public IPreProcessor Processor
        {
            get { return p; }
        }
    }    
}
