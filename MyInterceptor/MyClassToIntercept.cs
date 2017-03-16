using MyInterceptor.attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor
{    
    class MyClassToIntercept : Interceptable
    {
        public int Age { get;  private set; }
        public string Name { get; private set; }

        public MyClassToIntercept(int age=25, string name="Deepak")
        {
            Age = age;
            Name = name;
        }
        public int GetMyAge()
        {
            return Age;
        }

        public string GetMyName()
        {
            return Name;
        }

        [Log]
        public override string ToString()
        {
            return string.Format("Hi {0}, your age is {1}", Name, Age);
        }
    }
}
