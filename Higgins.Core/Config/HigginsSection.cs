using System.Runtime.Serialization;

namespace Higgins.Core.Config
{
    [DataContract]
    public class HigginsSection
    {
        public HigginsSection()
        {
            ConfigurationModule = true;
        }

        [DataMember(Name = "configuration_module")]
        public bool ConfigurationModule
        {
            get;
            set;
        }
    }
}
