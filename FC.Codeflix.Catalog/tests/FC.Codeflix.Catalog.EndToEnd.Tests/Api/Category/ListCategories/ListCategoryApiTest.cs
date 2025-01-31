﻿using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.ListCategories;

[Collection(nameof(ListCategoryApiTestFixture))]
public class ListCategoryApiTest
    : IDisposable
{
    private readonly ListCategoryApiTestFixture _fixture;

    public ListCategoryApiTest(ListCategoryApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(ListCategoriesAndTotalByDefault))]
    [Trait("EndToEnd/API", "Category/List - Endpoints")]
    public async void ListCategoriesAndTotalByDefault()
    {
        var defaultPerPage = 15;
        var exampleCategoriesList = _fixture.GetExampleCategoryList(20);
        await _fixture.Persistence.InsertList(exampleCategoriesList);

        var (response, output) = await _fixture.ApiClient
            .Get<ListCategoriesOutput>($"/categories");

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Total.Should().Be(exampleCategoriesList.Count);
        output.Items.Should().HaveCount(defaultPerPage);
        foreach(CategoryModelOutut outputItem in output.Items)
        {
            var exampleItem = exampleCategoriesList
                .FirstOrDefault(x => x.Id == outputItem.Id);
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }

    public void Dispose()
        => _fixture.CleanPersistence();
}
