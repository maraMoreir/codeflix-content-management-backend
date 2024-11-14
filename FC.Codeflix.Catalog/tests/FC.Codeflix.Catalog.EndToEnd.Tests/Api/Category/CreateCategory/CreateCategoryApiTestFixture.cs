using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTestFixtureCollection
    : ICollectionFixture<CreateCategoryApiTestFixture>
{ }

public class CreateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public CreateCategoryInput getExampleInput()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
        );
}
