using Autofac;
using Higgins.Core.Config;
using Nancy;

namespace Higgins.Web.Modules
{
    public class ConfigurationModule : NancyModule
    {
        public ConfigurationModule(ILifetimeScope scope)
        {
            ModulePath = "/config";
#if DEBUG
            Get["/dump"] = _ => scope.Resolve<HigginsConfigProvider>().Config;
#endif
        }
    }
}
