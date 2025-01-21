using FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.Common;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryApiTestFixture))]
public class GetCategortApiTestFixtureCollection
    : ICollectionFixture<GetCategoryApiTestFixture>
{ }

public class GetCategoryApiTestFixture 
    : CategoryBaseFixture
{ }
