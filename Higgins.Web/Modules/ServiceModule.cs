using Nancy;

namespace Higgins.Web.Modules
{
    public class ServiceModule : NancyModule
    {
        public ServiceModule()
        {
            ModulePath = "/s";
        }
    }
}