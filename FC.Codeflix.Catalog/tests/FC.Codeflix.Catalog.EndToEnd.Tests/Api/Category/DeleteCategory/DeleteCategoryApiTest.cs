﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryApiTestFixture))]
public class DeleteCategoryApiTest
    : IDisposable
{
    private readonly DeleteCategoryApiTestFixture _fixture;

    public DeleteCategoryApiTest(DeleteCategoryApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("EndToEnd/API", "Category/Delete - Endpoints")]
    public async void DeleteCategory()
    {
        //arrange: create a list of 20 example categories
        var exampleCategoriesList = _fixture.GetExampleCategoryList(20);
        await _fixture.Persistence.InsertList(exampleCategoriesList);
        var exampleCategory = exampleCategoriesList[10];

        //act: sends a DELETE request to the API to delete the selected category
        var (response, output) = await _fixture.ApiClient.Delete<object>(
            $"/categories/{exampleCategory.Id}"
        );

        //assert: checks whether the result is as expected
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status204NoContent);
        output.Should().BeNull();
        var persistenceCategory = await _fixture.Persistence
            .GetById(exampleCategory.Id);
        persistenceCategory.Should().BeNull();
    }

    [Fact(DisplayName = nameof(ErrorWhenNotFound))]
    [Trait("EndToEnd/API", "Category/Delete - Endpoints")]
    public async void ErrorWhenNotFound()
    {
        var exampleCategoriesList = _fixture.GetExampleCategoryList(20);
        await _fixture.Persistence.InsertList(exampleCategoriesList);
        var randomGuid = Guid.NewGuid();

        var (response, output) = await _fixture.ApiClient.Delete<ProblemDetails>(
            $"/categories/{randomGuid}"
        );

        //assert: checks whether the result is as expected NOT FOUND
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
        output.Should().NotBeNull();
        output!.Title.Should().Be("Not Found");
        output!.Type.Should().Be("Not Found");
        output!.Status.Should().Be((int)StatusCodes.Status404NotFound);
        output!.Detail.Should().Be($"Category '{randomGuid}' not found.");
    }
    public void Dispose()
        => _fixture.CleanPersistence();
}
