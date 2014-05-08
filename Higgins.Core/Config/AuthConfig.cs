using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Higgins.Core.Config
{
    [DataContract]
    public class AuthConfig
    {
        [DataMember(Name = "enable")]
        public bool Enable
        {
            get;
            set;
        }

        [DataMember(Name = "users")]
        public Dictionary<string, string> Users
        {
            get;
            set;
        }

        [DataMember(Name = "groups")]
        public Dictionary<string, IEnumerable<string>> Groups
        {
            get;
            set;
        }
    }
}
