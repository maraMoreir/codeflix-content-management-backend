namespace FC.Codeflix.Catalog.Application.Excpetion;
public class NotFoundException : ApplicationException
{
    public NotFoundException(string? message) : base(message)
    { }
}
