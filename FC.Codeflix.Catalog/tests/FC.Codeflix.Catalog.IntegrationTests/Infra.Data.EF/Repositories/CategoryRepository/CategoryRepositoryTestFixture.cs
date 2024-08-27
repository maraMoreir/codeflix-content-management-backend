using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.IntegrationTests.Base;
using Xunit;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTestFixureCollection
    : ICollectionFixture<CategoryRepositoryTestFixture>
{ }

public class CategoryRepositoryTestFixture 
    : BaseFixture
{
    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;

    }
    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];
        return categoryDescription;

    }
    public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public Category GetExampleCategory()
    => new(
        GetValidCategoryName(),
        GetValidCategoryDescription(),
        getRandomBoolean()
    );

    public
 }