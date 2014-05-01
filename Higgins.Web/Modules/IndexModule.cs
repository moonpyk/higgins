using Autofac;
using Nancy;
using Nancy.Owin;
using Nancy.Security;

namespace Higgins.Web.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule(ILifetimeScope scope)
        {
            Get["/"] = _ => View["index"];

            Get["/ping"] = _ => HttpStatusCode.OK;
        }
    }
}