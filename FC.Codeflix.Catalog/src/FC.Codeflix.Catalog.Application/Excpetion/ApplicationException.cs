namespace FC.Codeflix.Catalog.Application.Excpetion;
public abstract class ApplicationException : Exception
{
    protected ApplicationException(string? message) : base(message)
    { }
}
