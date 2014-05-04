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

            var rootPathProvider = scope.Resolve<IRootPathProvider>();

            Get["/log", true] = async (o, token) =>
            {
                var res = await Git.Log(rootPathProvider.GetRootPath());

                return res.Entries;
            };

            Get["/incoming", false] = async (o, token) =>
            {
                var res = await Git.Log(rootPathProvider.GetRootPath(), new[] { "HEAD..origin/master" });

                return res.Entries;
            };

#if DEBUG
            Get["/diag", true] = async (_, tk) =>
            {
                var res = await Git.Execute(rootPathProvider.GetRootPath(), new[] { "status" });

                return new
                {
                    _.project,
                    res
                };
            };


#endif

            Get["/foo"] = o => new RedirectResponse("/");
        }
    }
}
