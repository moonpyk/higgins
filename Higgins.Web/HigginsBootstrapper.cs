using Autofac;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Diagnostics;

namespace Higgins.Web
{
    public class HigginsBootstrapper : AutofacNancyBootstrapper
    {
#if DEBUG
        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {

                return new DiagnosticsConfiguration
                {
                    Password = "HigginsDebug"
                };
            }
        }
#endif
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
    }
}