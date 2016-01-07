using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using UnityAutoRegister;

namespace UnityAutoRegisterSample
{
    class Program
    {
        
        static void Main(string[] args)
        {
             
            var c = new UnityContainer();
            AutoRegister.All(c);
            //AutoRegister.All() perform  the following on each object with the "Implement"
            //attribute.

            //c.RegisterType(typeof (IUserService), typeof (UserService));


            var service = c.Resolve<UserViewModel>();

            if (service.Authorize("test", "test"))
            {
                Console.WriteLine("OK");
            }

            Console.ReadLine();



        }
    }
}
