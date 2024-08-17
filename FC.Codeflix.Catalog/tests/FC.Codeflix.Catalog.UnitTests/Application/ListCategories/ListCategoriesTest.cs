using FC.Codeflix.Catalog.Domain.Entity;
using Moq;
using Xunit;

namespace FC.Codeflix.Catalog.UnitTests.Application.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest
{
    private readonly ListCategoriesTestFixture _fixture;

    public ListCategoriesTest(ListCategoriesTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(List))]
    [Trait("Application", "ListCategories - Use Cases ")]
    public async Task List()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = new ListCategoriesInput(
            page: 2,
            perPage: 15,
            search: "search-example",
            sort: "name",
            dir: SearchOrder.Asc
            );
        var outputRepositorySearch = new OutpurSearch<Category>(
                    currentPage: input.Page,
                    perPage: input.Perpage,
                    Items: (IReadOnlyList<Category>)_fixture.GetExampleCategoriesList(),
                    Total: 70
        );

        repositoryMock.Setup(x => x.Search(
            It.IsAny<SearchInput>(
                searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
                ),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NorBeNull();
        output.Page.Should().Be(outputRepositorySearch.currentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        output.Items.Foreach(outputItem =>
        {
            var repositoryCategory = outputRepositorySearch.Items
                .Find(Xunit => Xunit.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryCategory.Name);
            outputItem.Description.Should().Be(repositoryCategory.Description);
            outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
            outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
        });

        repositoryMock.Verify(x => x.Search(
            It.IsAny<SearchInput>(
                searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
                ),
            It.IsAny<CancellationToken>()
            ), Times.Once);
    }
}