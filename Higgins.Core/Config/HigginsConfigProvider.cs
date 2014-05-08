using System;
using System.ComponentModel.Design;
using System.Configuration;
using System.IO;
using Autofac;
using Nancy;

namespace Higgins.Core.Config
{
    public class HigginsConfigProvider
    {
        private readonly string _configPath;
        private FileSystemWatcher _watcher;

        public HigginsConfigProvider(ILifetimeScope container)
        {
            var rootPath = container.Resolve<IRootPathProvider>().GetRootPath();
            _configPath = Path.Combine(rootPath, "Config", "higgins.json");

            if (!Read())
            {
                throw new InvalidOperationException("Unable to open Higgins configuration.");
            }

            // _watcher = new FileSystemWatcher(_configPath);
            // _watcher.Changed += (sender, args) => Read();
        }

        public bool Read()
        {
            var temp = HigginsConfig.Parse(_configPath);

            if (temp == null)
            {
                return false;
            }

            Config = temp;
            return true;
        }

        public HigginsConfig Config
        {
            get;
            private set;
        }
    }
}
