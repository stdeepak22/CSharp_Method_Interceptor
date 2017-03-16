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
        private List<IPreProcessor> preProcessors = new List<IPreProcessor>();
        public PreProcessAttribute(params Type []preProcessorTypes)
        {
            preProcessorTypes.ToList().ForEach(p =>
            {
                var processor = Activator.CreateInstance(p) as IPreProcessor;
                if (processor == null)
                    throw new ArgumentException(String.Format("The type '{0}' does not implement interface IPreProcessor", p.Name));
                this.preProcessors.Add(processor);
            });            
        }

        public List<IPreProcessor> Processors
        {
            get { return preProcessors; }
        }
    }    
}
