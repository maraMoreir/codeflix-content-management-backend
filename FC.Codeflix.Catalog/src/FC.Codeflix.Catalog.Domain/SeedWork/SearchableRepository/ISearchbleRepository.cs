namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
public interface ISearchbleRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    Task<SearchOutput<TAggregate>> Search(
        SearchInput input,
        CancellationToken cancellationToken
        );
}
