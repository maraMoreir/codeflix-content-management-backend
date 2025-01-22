using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryApiTestFixture))]
public class DeleteCategoryApiTestFixtureCollection
    : ICollectionFixture<DeleteCategoryApiTestFixture>
{ }

public class DeleteCategoryApiTestFixture
    : CategoryBaseFixture
{ }
