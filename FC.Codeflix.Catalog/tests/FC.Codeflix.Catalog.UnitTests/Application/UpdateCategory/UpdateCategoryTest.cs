using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(UpdateCategory))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    public async Task UpdateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var exampleCategory = _fixture.GetExampleCategory();
        repositoryMock.Setup(x => x.Get(
            exampleCategory.Id,
            It.IsAny<CancellationToken>())
    ).ReturnsAsync(exampleCategory);
        var input = UseCase.UpdateCategoryInput(
           exampleCategory.Id,
           _fixture.GetValidCategoryName(),
           _fixture.GetValidCategoryDescription(),
           exampleCategory.IsActive
        );
        var useCase = new UseCase.UpdateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        CategoryModelOutut outut = await useCase.Handle(input);

        outut.Should().NotBeNull();
        outut.Name.Should().Be(input.Name);
        outut.Description.Should().Be(input.Description);
        outut.IsActive.Should().Be(input.IsActive);
        repositoryMock.Verify(x => x.Get(
            exampleCategory.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);
        repositoryMock.Verify(x => x.Update(
            exampleCategory.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
