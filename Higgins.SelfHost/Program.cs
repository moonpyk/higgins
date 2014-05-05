using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgins.Web;
using Microsoft.Owin.Hosting;
using Nancy.Bootstrapper;

namespace Higgins.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomainAssemblyTypeScanner.LoadAssemblies(new RelativePathProvider().GetRootPath(), "*.dll");
            
            var options = new StartOptions
            {
                ServerFactory = "Nowin",
                Port = 8080,
            };

            using (WebApp.Start<HigginsStartupSelfHost>(options))
            {
                Console.WriteLine("Running a http server on port 8080");
                Console.ReadKey();
            }
        }
    }
}
