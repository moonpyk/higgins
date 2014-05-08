﻿using System.Threading;
using System.Threading.Tasks;
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

            var hcp = scope.Resolve<HigginsConfigProvider>();

            Before += ctx => !hcp.Config.Higgins.ConfigurationModule 
                ? new NotFoundResponse() 
                : null;

#if DEBUG
            Get["/dump"] = _ =>
            {
                return hcp.Config;
            };
#endif
        }
    }
}
