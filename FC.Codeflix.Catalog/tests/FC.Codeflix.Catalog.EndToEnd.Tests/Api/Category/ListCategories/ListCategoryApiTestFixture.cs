using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoryApiTestFixture))]
public class ListCategoryApiTestFixtureCollection
    : ICollectionFixture<ListCategoryApiTestFixture>
{ }

public class ListCategoryApiTestFixture
    : CategoryBaseFixture
{
    public List<DomainEntity.Category> GetExampleCategoriesListWithNames(
        List<string> names
    ) => names.Select(name =>
    {
        var category = GetExampleCategory();
        category.Update(name);
        return category;
    }).ToList();

    public List<DomainEntity.Category> CloneCategoriesListOrderd(
        List<DomainEntity.Category> categoriesList,
        string orderBy,
        SearchOrder order
    )
    {
        var listClone = new List<DomainEntity.Category>(categoriesList);
        var orderdEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
            _ => listClone.OrderBy(x => x.Name),
        };
        return orderdEnumerable
            .ThenBy(x => x.CreatedAt).ToList();
    }
}
