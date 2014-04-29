using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Nancy.Owin;
using Owin;

[assembly: OwinStartup(typeof(Higgins.Web.HigginsStartup))]

namespace Higgins.Web
{
    public class HigginsStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
