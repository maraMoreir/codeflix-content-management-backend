namespace FC.Codeflix.Catalog.Application.Excpetions;
public abstract class ApplicationException : Exception
{
    protected ApplicationException(string? message) : base(message)
    { }
}
