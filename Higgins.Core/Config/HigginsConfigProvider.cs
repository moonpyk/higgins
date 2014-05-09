using Autofac;
using Nancy;
using System;
using System.IO;

namespace Higgins.Core.Config
{
    public class HigginsConfigProvider : IDisposable
    {
        private readonly string _configPath;

        public HigginsConfigProvider(ILifetimeScope container)
        {
            var rootPath = container.Resolve<IRootPathProvider>().GetRootPath();
            _configPath = Path.Combine(rootPath, "Config", "higgins.json");

            if (!Read())
            {
                throw new InvalidOperationException("Unable to open Higgins configuration.");
            }
        }

        public bool Read()
        {
            var temp = Configuration.Parse(_configPath);

            if (temp == null)
            {
                return false;
            }

            Config = temp;
            return true;
        }

        public Configuration Config
        {
            get;
            private set;
        }

        public void Dispose()
        {
        }
    }
}
