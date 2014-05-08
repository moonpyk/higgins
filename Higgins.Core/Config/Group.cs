using System.Collections.Generic;

namespace Higgins.Core.Config
{
    public class Group
    {
        public string Name
        {
            get;
            set;
        }

        public IEnumerable<string> Members
        {
            get;
            set;
        }
    }
}
