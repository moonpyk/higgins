using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Higgins.Core.Config
{
    [DataContract]
    public class Project
    {
        public Project()
        {
            Upstream       = "origin";
            UpstreamBranch = "origin/master";
            Users          = new[] { "*" };
            Groups         = new[] { "*" };
        }

        [DataMember(Name = "repo_path")]
        public string RepoPath
        {
            get;
            set;
        }

        [DataMember(Name = "upstream")]

        public string Upstream
        {
            get;
            set;
        }

        [DataMember(Name = "upstream_branch")]
        public string UpstreamBranch
        {
            get;
            set;
        }

        [DataMember(Name = "users")]
        public IEnumerable<string> Users
        {
            get;
            set;
        }

        [DataMember(Name = "groups")]
        public IEnumerable<string> Groups
        {
            get;
            set;
        }
    }
}
