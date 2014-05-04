namespace Higgins.Core
{
    public class GitRawResult : IGitResult
    {
        public string StandardOutput
        {
            get;
            set;
        }

        public string StandardError
        {
            get;
            set;
        }

        public string Output
        {
            get;
            set;
        }

        public int Code
        {
            get;
            set;
        }
    }
}