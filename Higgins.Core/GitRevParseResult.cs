namespace Higgins.Core
{
    public class GitRevParseResult : IGitResult
    {
        public GitRevParseResult(int code, string output)
        {
            Code = code;
            Output = Revision = output;
        }

        public string Revision { get; private set; }
        public int Code { get; private set; }
        public string Output { get; private set; }
    }
}