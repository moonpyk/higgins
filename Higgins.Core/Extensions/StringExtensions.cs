using System.Collections.Generic;
using System.Linq;

namespace Higgins.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsStar(this IEnumerable<string> s)
        {
            return s != null && s.Contains("*");
        }
    }
}