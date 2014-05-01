using Autofac;
using Nancy;

namespace Higgins.Web.Modules
{
    public class ServiceModule : NancyModule
    {
        public ServiceModule(ILifetimeScope scope)
        {
            ModulePath = "/s/{project?#default#}";

#if DEBUG
            Get["/diag"] = _ => _;
#endif
        }
    }
}
