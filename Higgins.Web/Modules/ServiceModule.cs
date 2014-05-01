using Autofac;
using Nancy;

namespace Higgins.Web.Modules
{
    public class ServiceModule : NancyModule
    {
        public ServiceModule(ILifetimeScope scope)
        {
            ModulePath = "/s";
        }
    }
}
