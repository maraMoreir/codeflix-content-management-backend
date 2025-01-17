﻿using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.CreateCategory;

[Collection(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTest
{
    private readonly CreateCategoryApiTestFixture _fixture;

    public CreateCategoryApiTest(CreateCategoryApiTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("EndToEnd/API", "Category - Endpoints")]
    public async Task CreateCategory()
    {
        var input = _fixture.getExampleInput();

        var (response, output) = await _fixture
            .ApiClient.Post<CategoryModelOutut>(
                "/categories",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        output.Should().NotBeNull();
        output!.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should()
            .NotBeSameDateAs(default);
        var dbCategory = await _fixture
            .Persistence.GetById(output.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(input.Name);
        dbCategory.Description.Should().Be(input.Description);
        dbCategory.IsActive.Should().Be(input.IsActive);
        dbCategory.Id.Should().NotBeEmpty();
        dbCategory.CreatedAt.Should()
            .NotBeSameDateAs(default);
    }
}
