﻿using FC.Codeflix.Catalog.Domain.Entity;
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

        [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async Task CreateCategoryWithOnlyName()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object, unitOfWorkMock.Object
            );

            var input = new CreateCategoryInput(
                _fixture.GetValidCategoryName()
            );

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
            output.Description.Should().Be("");
            output.IsActive.Should().BeTrue();
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async Task CreateCategoryWithOnlyNameAndDescription()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new UseCases.CreateCategory(
                repositoryMock.Object, unitOfWorkMock.Object
            );

            var input = new CreateCategoryInput(
                _fixture.GetValidCategoryName(),
                _fixture.GetValidCategoryDescription()
            );

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
            output.IsActive.Should().BeTrue();
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(
            nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
            parameters:12,
            MemberType = typeof(CreateCategoryTestDataGenerator)
        )]
        public async Task ThrowWhenCantInstantiateCategory(
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
    }
}