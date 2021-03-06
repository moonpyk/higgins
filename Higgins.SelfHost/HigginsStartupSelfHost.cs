﻿using Higgins.Web;
using Nancy;
using Nancy.Owin;
using Owin;
using System;
using System.IO;

namespace Higgins.SelfHost
{
    class HigginsStartupSelfHost
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(new NancyOptions
            {
                Bootstrapper = new HigginsBootstrapper(new RelativePathProvider()),
            });
        }
    }

    public class RelativePathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                ".."
            ));
        }
    }
}
