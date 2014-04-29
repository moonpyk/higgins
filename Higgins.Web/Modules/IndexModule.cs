using Nancy;
using Nancy.Security;

namespace Higgins.Web.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => View["index"];

            Get["/ping"] = _ => HttpStatusCode.OK;
        }
    }
}