using Autofac;
using Higgins.Web.Lib;
using Nancy;

namespace Higgins.Web.Modules
{
    public class ServiceModule : NancyModule
    {
        private const string DefaultProjectString = "#default#";

        public ServiceModule(ILifetimeScope scope)
        {
            ModulePath = string.Format("/s/{{project?{0}}}", DefaultProjectString);

#if DEBUG
            Get["/diag", true] = async (_, tk) =>
            {
                var res = await Git.Execute(scope.Resolve<IRootPathProvider>().GetRootPath(), new[] { "status" });

                return res;
            };
#endif
        }
    }
}
