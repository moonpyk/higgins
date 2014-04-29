using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgins.Web;
using Microsoft.Owin.Extensions;
using Nancy;
using Nancy.Owin;
using Owin;

namespace Higgins.SelfHost
{
    class HigginsStartupSelfHost
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(new NancyOptions
            {
                Bootstrapper = new HigginsBootstrapper(new MyRootPathProvider())
            });
        }

        class MyRootPathProvider : IRootPathProvider
        {
            public string GetRootPath()
            {
                return Path.GetFullPath(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "..\\..\\..\\Higgins.Web\\"
                ));
            }
        }
    }
}
