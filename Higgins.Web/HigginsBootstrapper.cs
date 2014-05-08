using System.Diagnostics;
using System.IO;
using Autofac;
using Higgins.Core.Config;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.Hosting.Aspnet;
using Newtonsoft.Json;

namespace Higgins.Web
{
    public class HigginsBootstrapper : AutofacNancyBootstrapper
    {
        private readonly IRootPathProvider _rootPathProvider;

        public HigginsBootstrapper()
            : this(null)
        { }

        public HigginsBootstrapper(IRootPathProvider pv)
        {
            _rootPathProvider = pv;
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return _rootPathProvider ?? base.RootPathProvider;
            }
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Scripts"));
            base.ConfigureConventions(nancyConventions);
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope scope)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HigginsConfigProvider>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Update(scope.ComponentRegistry);

            base.ConfigureApplicationContainer(scope);
        }

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
