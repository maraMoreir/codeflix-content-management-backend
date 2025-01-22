using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
using FluentAssertions;
using Xunit;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("Application", "DeleteCategory - UseCases")]
    public async Task DeleteCategory()
    {
        //arrange: set up the mocks and inputs for the test
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock(); 
        var exampleGuid = Guid.NewGuid(); 
        var categoryExample = _fixture.GetExampleCategory(); 
        //set up the repository mock to return the example category when requested
        repositoryMock.Setup(x => x.Get(
            categoryExample.Id,
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(categoryExample);
        //prepare the input with the category id to delete
        var input = new UseCase.DeleteCategoryInput(categoryExample.Id);
        //create the use case with the mocked dependencies
        var useCase = new UseCase.DeleteCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        //act: execute the use case to delete the category
        await useCase.Handle(input, CancellationToken.None);

        //assert: verify that the repository methods and unit of work methods were called correctly
        repositoryMock.Verify(x => x.Get(
            categoryExample.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once); 
        repositoryMock.Verify(x => x.Delete(
            categoryExample,
            It.IsAny<CancellationToken>()
        ), Times.Once); 
        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()
        ), Times.Once()); 
    }

    [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
    [Trait("Application", "DeleteCategory - UseCases")]
    public async Task ThrowWhenCategoryNotFound()
    {
        //arrange: set up the mocks for the failure case
        var repositoryMock = _fixture.GetRepositoryMock(); 
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock(); 
        var exampleGuid = Guid.NewGuid(); 
        repositoryMock.Setup(x => x.Get(
            exampleGuid,
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(
            new DirectoryNotFoundException($"Category '{exampleGuid}' not found.")
        );
        var input = new UseCase.DeleteCategoryInput(exampleGuid);
        var useCase = new UseCase.DeleteCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        //act: execute the use case
        var task = async () => await useCase.Handle(input, CancellationToken.None);

        //assert: verify
        await task.Should().ThrowAsync<DirectoryNotFoundException>();
        repositoryMock.Verify(x => x.Get(
            exampleGuid,
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
