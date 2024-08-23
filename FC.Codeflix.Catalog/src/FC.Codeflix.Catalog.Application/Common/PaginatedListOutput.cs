namespace FC.Codeflix.Catalog.Application.Common;
public abstract class PaginatedListOutput<TOutputItems>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TOutputItems> Items { get; set; }

    public PaginatedListOutput(
        int page,
        int perPage, 
        int total,
        IReadOnlyList<TOutputItems> items)
    {
        Page = page;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
}
