using System;

namespace Higgins.Core
{
    public class InvalidGitResultException : Exception
    {
        public int GitCode
        {
            get;
            private set;
        }

        public InvalidGitResultException(int code)
            : base(string.Format("Git returned code : {0}", code))
        {
            GitCode = code;
        }
    }
}