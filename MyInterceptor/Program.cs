using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new MyClassToIntercept();
            Console.WriteLine(user.Age);
            Console.WriteLine(user.Name);
            
            //Here we should get Pre processor in output window.
            var result = user.ToString();
            //Here we should get Post processor in output window.
            
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }    
}
