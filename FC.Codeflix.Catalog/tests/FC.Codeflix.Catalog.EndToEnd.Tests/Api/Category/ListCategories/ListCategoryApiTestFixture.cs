using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoryApiTestFixture))]
public class ListCategoryApiTestFixtureCollection
    : ICollectionFixture<ListCategoryApiTestFixture>
{ }

public class ListCategoryApiTestFixture 
    : CategoryBaseFixture
{ }
