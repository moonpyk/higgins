using Autofac;
using Higgins.Core;
using Nancy;
using Nancy.Responses;

namespace Higgins.Web.Modules
{
    public class ServiceModule : NancyModule
    {
        private const string DefaultProjectString = "[default]";

        public ServiceModule(ILifetimeScope scope)
        {
            ModulePath = string.Format("/s/{{project?{0}}}", DefaultProjectString);

#if DEBUG
            Get["/diag", true] = async (_, tk) =>
            {
                var res = await Git.Execute(scope.Resolve<IRootPathProvider>().GetRootPath(), new[] { "status" });

                return new
                {
                    _.project,
                    res
                };
            };

            Get["/log", true] = async (o, token) =>
            {
                return await Git.Log(scope.Resolve<IRootPathProvider>().GetRootPath());
            };
#endif

            Get["/foo"] = o => new RedirectResponse("/");
        }
    }
}
