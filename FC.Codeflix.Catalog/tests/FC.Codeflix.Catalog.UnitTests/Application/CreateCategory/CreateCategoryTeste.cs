using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FC.Codeflix.Catalog.Application.UseCases.CreateCategory;
namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory;

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var respositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IuniOfMock>();
        var useCase = new UseCases.CreateCategory(
            respositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(
            "Category Name",
            "Category Description",
            true
            );

        var output = await useCase.Handle(input);

        output.ShouldNotBeNull();
        output.Name.Should().Be("Category Name");
        output.Description.Should().Be("Category Description");
        output.IsActive.Should().Be(true);
        (output.Id != null && output.Id != Guid.Empty).Should().BeTrue();
        (output.CreatedAt != null && output.CreatedAt != default(DateTime)).Should().BeTrue();
}
