using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.Common;
using FC.Codeflix.Catalog.Api.ApiModels.Category;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryApiTestFixture))]
public class UpdateCategoryApiTestCollection
    : ICollectionFixture<UpdateCategoryApiTestFixture>
{ }

public class UpdateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public UpdateCategoryApiInput GetExampleInput()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
        );
}
