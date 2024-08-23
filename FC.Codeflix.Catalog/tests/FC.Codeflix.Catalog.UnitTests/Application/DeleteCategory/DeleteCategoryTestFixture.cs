using FC.Codeflix.Catalog.UnitTests.Application.Common;
using Xunit;

namespace FC.Codeflix.Catalog.UnitTests.Application.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]

public class DeleteCategoryTestFixtureCollection
    : ICollectionFixture<DeleteCategoryTestFixture>
{ }
public class DeleteCategoryTestFixture 
    : CategoryUseCasesBaseFixture
{ }
