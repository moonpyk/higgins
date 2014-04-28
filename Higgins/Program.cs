using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgins.Web;
using Microsoft.Owin.Hosting;

namespace Higgins
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new StartOptions
            {
                ServerFactory = "Nowin",
                Port = 8080,               
            };

            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Running a http server on port 8080");
                Console.ReadKey();
            }
        }
    }
}
