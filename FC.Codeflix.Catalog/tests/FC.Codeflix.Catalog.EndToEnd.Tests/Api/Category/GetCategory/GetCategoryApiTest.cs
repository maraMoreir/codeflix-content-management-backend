﻿using FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.GetCategory;
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.EndToEnd.Tests.Extensions.DateTime;
using FC.Codeflix.Catalog.Api.ApiModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.GetCategoryById;
[Collection(nameof(GetCategoryApiTestFixture))]
public class GetCategoryApiTest
    : IDisposable
{
    private readonly GetCategoryApiTestFixture _fixture;

    public GetCategoryApiTest(GetCategoryApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("EndToEnd/API", "Category/Get - Endpoints")]
    public async Task GetCategory()
    {
        //arrange: insert category list into the bank
        var exampleCategoriesList = _fixture.GetExampleCategoryList();
        await _fixture.Persistence.InsertList(exampleCategoriesList);
        var exampleCategory = exampleCategoriesList[10];

        //act: get the specific category
        var (response, output) = await _fixture.ApiClient
            .Get<ApiResponse<CategoryModelOutut>>(
            $"/categories/{exampleCategory.Id}"
        );

        //assert: check the search result
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode) StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output.Data!.Id.Should().Be(exampleCategory.Id);
        output.Data.Name.Should().Be(exampleCategory.Name);
        output.Data.Description.Should().Be(exampleCategory.Description);
        output.Data.IsActive.Should().Be(exampleCategory.IsActive);
        output.Data.CreatedAt.TrimMilisseconds().Should().Be(
                exampleCategory.CreatedAt.TrimMilisseconds()
        );
    }

    [Fact(DisplayName = nameof(ErrorWhenNotFound))]
    [Trait("EndToEnd/API", "Category/Get - Endpoints")]
    public async Task ErrorWhenNotFound()
    {
        //arrange: generate a list of 20 exampple de categories
        var exampleCategoriesList = _fixture.GetExampleCategoryList(20);
        await _fixture.Persistence.InsertList(exampleCategoriesList);
        var randomGuid = Guid.NewGuid();

        //act: get a category using radom GUID
        var (response, output) = await _fixture.ApiClient.Get<ProblemDetails>(
            $"/categories/{randomGuid}"
        );

        //assert: check the search result
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
        output.Should().NotBeNull();
        output!.Status.Should().Be((int)StatusCodes.Status404NotFound);
        output.Title.Should().Be("Not Found");
        output.Type.Should().Be("Not Found");
        output.Detail.Should().Be($"Category '{randomGuid}' not found.");
    }

    public void Dispose()
        => _fixture.CleanPersistence();
}
