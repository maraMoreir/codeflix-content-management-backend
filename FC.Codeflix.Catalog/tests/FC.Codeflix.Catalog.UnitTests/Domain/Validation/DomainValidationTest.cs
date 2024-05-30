using Bogus;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    // não ser null
    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var value = Faker.Commerce.ProductName();
        Action action =
            () => DomainValidation.NotNull(value, fieldName);
        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThowWhenNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThowWhenNull()
    {
        string? value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action =
            () => DomainValidation.NotNull(value, fieldName);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null");
    }

    //não ser null ou vazio
    [Theory(DisplayName = nameof(NotNullThrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]

    public void NotNullThrowWhenEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null or empty");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validation")]

    public void NotNullOrEmptyOk()
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var target = Faker.Commerce.ProductName();

        Action action =
            () => DomainValidation.NotNullOrEmpty(target, fieldName);
        action.Should().NotThrow();
    }


    // tamanho minimo
    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThenMin), parameters: 10)]
    public void MinLengthThrowWhenLess(string target, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = 
            () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be less than {minLength} characters long");
    }

    public static IEnumerable<object[]> GetValuesSmallerThenMin(int numberOftests = 5)
    {
        yield return new object[] { "123456", 10 };
        var faker = new Faker();
        for (int i = 0; i < (numberOftests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length + (new Random()).Next(1, 20);
            yield return new object[] { example, minLength };
        }
    }

    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
    public void MinLengthOk(string target, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOftests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < (numberOftests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length - (new Random()).Next(1, 5);
            yield return new object[] { example, minLength };
        }
    }

    //tamanho máximo
    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthThrowWhenGreater(string target, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be greater than {maxLength} characters long");
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOftests = 5)
    {
        yield return new object[] { "123456", 5 };
        var faker = new Faker();
        for (int i = 0; i < (numberOftests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length - (new Random()).Next(1, 5);
            yield return new object[] { example, maxLength };
        }
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
    public void MaxLengthOk(string target, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
           () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().NotThrow<EntityValidationException>();
    }

    public static IEnumerable<object[]> GetValuesLessThanMax(int numberOftests = 5)
    {
        yield return new object[] { "12345", 6 }; 
        var faker = new Faker();
        for (int i = 0; i < (numberOftests - 1); i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length + (new Random()).Next(1, 5);
            yield return new object[] { example, maxLength };
        }
    }
};
