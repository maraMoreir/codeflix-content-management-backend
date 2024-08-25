using FC.Codeflix.Catalog.IntegrationTests.Base;
using Xunit;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTestFixureCollection
    : ICollectionFixture<CategoryRepositoryTestFixture>
{ }

public class CategoryRepositoryTestFixture 
    : BaseFixture
{ }
