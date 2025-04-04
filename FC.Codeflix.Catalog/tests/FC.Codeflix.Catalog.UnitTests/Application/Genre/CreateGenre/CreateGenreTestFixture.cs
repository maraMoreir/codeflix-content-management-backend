﻿using FC.Codeflix.Catalog.Application.UseCases.Genre.CreateGenre;
using FC.Codeflix.Catalog.UnitTests.Application.Genre.Common;
using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using Xunit;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Genre.CreateGenre;
[CollectionDefinition(nameof(CreateGenreTestFixture))]
public class GenreUseCasesBaseFixtureCollection
    : ICollectionFixture<CreateGenreTestFixture>
{ }

public class CreateGenreTestFixture
    : GenreUseCasesBaseFixture
{
    public CreateGenreInput GetExampleInput()
        => new CreateGenreInput(
            GetValidGenreName(),
            GetRandomBoolean()
        );

    public CreateGenreInput GetExampleInputWithCategories()
    {
        var numberOfCategoriesIds = (new Random()).Next(1, 10);
        var categoriesIds = Enumerable.Range(1, numberOfCategoriesIds)
            .Select(_ => Guid.NewGuid())
            .ToList();
        return new(
            GetValidGenreName(),
            GetRandomBoolean()
        );
    }

    public Mock<IGenreRepository> GetGenreRepositoryMock()
        => new Mock<IGenreRepository>();

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new Mock<IUnitOfWork>();
}
