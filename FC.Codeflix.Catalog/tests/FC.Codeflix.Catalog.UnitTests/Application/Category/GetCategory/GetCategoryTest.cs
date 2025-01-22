using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FC.Codeflix.Catalog.Application.Excpetion;
using FluentAssertions;
using Xunit;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("Application", "GetCategory - Use Cases")]
    public async Task GetCategory()
    {
        //arrange: sets up mocks, input, and use case instance
        var repositoryMock = _fixture.GetRepositoryMock(); 
        var exampleCategory = _fixture.GetExampleCategory(); 
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(), 
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(exampleCategory); 
        var input = new UseCase.GetCategoryInput(exampleCategory.Id); //create input for the use case
        var useCase = new UseCase.GetCategory(repositoryMock.Object); 

        //act: executes the use case
        var output = await useCase.Handle(input, CancellationToken.None); 

        // assert: validates the expected results
        repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(), 
            It.IsAny<CancellationToken>()
            ), Times.Once); 
        output.Should().NotBeNull(); 
        output.Name.Should().Be(exampleCategory.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        output.Id.Should().Be(exampleCategory.Id); 
        output.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoestExist))]
    [Trait("Application", "GetCategory - Use Cases")]
    public async Task NotFoundExceptionWhenCategoryDoestExist()
    {
        //arrange: sets up mocks, input, and use case instance
        var repositoryMock = _fixture.GetRepositoryMock(); //creates repository mock
        var exampleGuid = Guid.NewGuid(); // creates a new guid for the test
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(), 
            It.IsAny<CancellationToken>()
        )).ThrowsAsync( //sets up the mock to throw an exception when Get is called
            new NotFoundException($"Category '{exampleGuid}' not found") 
        );
        var input = new UseCase.GetCategoryInput(exampleGuid);
        var useCase = new UseCase.GetCategory(repositoryMock.Object); 

        // act: executes the action, i.e., the use case with the configured input
        var task = async ()
            => await useCase.Handle(input, CancellationToken.None); 

        // assert: validates if the expected exception was thrown
        await task.Should().ThrowAsync<NotFoundException>(); // checks if the NotFoundException is thrown
        repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(), // verifies that Get method is called with any guid
            It.IsAny<CancellationToken>()
            ), Times.Once); // verifies that it was called once
    }
}
