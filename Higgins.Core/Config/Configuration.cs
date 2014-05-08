using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Higgins.Core.Config
{
    [DataContract]
    public class Configuration
    {
        public Configuration()
        {
            Auth = new AuthSection
            {
                Enable = true
            };

            Higgins = new HigginsSection
            {
                ConfigurationModule = false
            };
        }

        [DataMember(Name = "auth")]
        public AuthSection Auth
        {
            get;
            set;
        }

        [DataMember(Name = "higgins")]
        public HigginsSection Higgins
        {
            get;
            set;
        }

        [DataMember(Name = "projects")]
        public Dictionary<string, Project> Projects
        {
            get;
            set;
        }

        public string ToJson(Formatting f)
        {
            return JsonConvert.SerializeObject(this, f);
        }

        public static Configuration Parse(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(path));

            }
            catch (Exception) { }

            return null;
        }
    }
}
