﻿using System;
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
    public class HigginsConfig
    {
        [DataMember(Name = "auth")]
        public AuthConfig Auth
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

        public static HigginsConfig Parse(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<HigginsConfig>(File.ReadAllText(path));

            }
            catch (Exception) { }

            return null;
        }
    }
}
