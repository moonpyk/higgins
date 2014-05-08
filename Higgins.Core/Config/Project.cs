using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgins.Core.Config
{
    public class Project
    {
        public Project()
        {
            Upstream       = "origin";
            UpstreamBranch = "origin/master";
            Users          = new[] { "*" };
            Groups         = new[] { "*" };
        }

        public string Name
        {
            get;
            set;
        }

        public string RepoPath
        {
            get;
            set;
        }

        public string Upstream
        {
            get;
            set;
        }

        public string UpstreamBranch
        {
            get;
            set;
        }

        public IEnumerable<string> Users
        {
            get;
            set;
        }

        public IEnumerable<string> Groups
        {
            get;
            set;
        }
    }

}
