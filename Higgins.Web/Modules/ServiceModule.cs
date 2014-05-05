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

            var rpp = scope.Resolve<IRootPathProvider>();

            Get["/status", true] = async (_, token) =>
            {
                var res = await Git.RevParse(rpp.GetRootPath(), new[] { "HEAD" });

                if (res.Code != 0)
                {
                    throw new InvalidGitResultException(res.Code);
                }

                return res.Revision;
            };

            Get["/log", true] = async (_, token) =>
            {
                var res = await Git.Log(rpp.GetRootPath());

                if (res.Code != 0)
                {
                    throw new InvalidGitResultException(res.Code);
                }

                return res.Entries;
            };

            Get["/incoming", false] = async (_, token) =>
            {
                var upstream = "origin/master";

                var res = await Git.Log(
                    rpp.GetRootPath(),
                    new[] { string.Format("HEAD..{0}", upstream) }
                );

                if (res.Code != 0)
                {
                    throw new InvalidGitResultException(res.Code);
                }

                return res.Entries;
            };

#if DEBUG
            Get["/diag", true] = async (_, tk) =>
            {
                var res = await Git.Execute(rpp.GetRootPath(), new[] { "status" });

                return new
                {
                    _.project,
                    res
                };
            };
#endif
        }
    }
}
