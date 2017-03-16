using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor2.attr.other
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class PostProcessAttribute : Attribute
    {
        private List<IPostProcessor> postProcessors = new List<IPostProcessor>();
        public PostProcessAttribute(params Type[] postProcessorTypes)
        {
            postProcessorTypes.ToList().ForEach(p =>
            {
                var processor = Activator.CreateInstance(p) as IPostProcessor;
                if (processor == null)
                    throw new ArgumentException(String.Format("The type '{0}' does not implement interface IPostProcessor", p.Name));
                this.postProcessors.Add(processor);
            });            
        }

        public List<IPostProcessor> Processors
        {
            get { return postProcessors; }
        }
    }
}
