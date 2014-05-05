namespace Higgins.Core
{
    public interface IGitResult
    {
        int Code { get; }
        string Output { get;  }
    }
}