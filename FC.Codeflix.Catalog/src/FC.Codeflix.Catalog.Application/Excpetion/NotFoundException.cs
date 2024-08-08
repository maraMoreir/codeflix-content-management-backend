namespace FC.Codeflix.Catalog.Application.Excpetions;
public class NotFoundException : ApplicationException
{
    public NotFoundException(string? message) : base(message)
    { }
}
