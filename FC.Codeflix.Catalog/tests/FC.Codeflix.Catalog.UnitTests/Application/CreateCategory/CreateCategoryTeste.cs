using FC.Codeflix.Catalog.Domain.Entity;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCategoryTestFixture _fixture;
        public CreateCategoryTest(CreateCategoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async Task CreateCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock(); 
            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object, unitOfWorkMock.Object
            );

            var input = _fixture.GetInput();

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAggregateAsync))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(nameof(GetInvalidInputs))]
        public async Task ThrowWhenCantInstantiateAggregateAsync(
            CreateCategoryInput input,
            string exceptionMessage
        )
        {
            var useCase = new UseCases.CreateCategory(
                _fixture.GetRepositoryMock().Object,
                _fixture.GetUnitOfWorkMock().Object

            );

            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

             await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);
        }

        private static IEnumerable<object[]> GetInvalidInputs()
        {
            var fixture = new CreateCategoryTestFixture();
            var invalidInputsList = new List<object[]>();

            var invalidInputShortName = fixture.GetInput();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            invalidInputsList.Add(new object[]
            {
                invalidInputShortName,
                "Name shoud be at leats 3 characters long"
            });

            var invalidInputTooLongName = fixture.GetInput();
            var tooLongNameFoirCategory = fixture.Faker.Commerce.ProductName();
            while (tooLongNameFoirCategory.Length < 255)
                tooLongNameFoirCategory = $"{tooLongNameFoirCategory} {fixture.Faker.Commerce.ProductName()}";
            invalidInputTooLongName.Name = invalidInputTooLongName.Name.Substring(0, 2);
            invalidInputsList.Add(new object[]
            {
                invalidInputTooLongName,
                "Name shoud be less or equal 255 characters long"
            });

            return invalidInputsList;
        }
    }
}
