using Xunit;

namespace Reda.Domain.Tests;

public class ProductIdTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsDefault()
    {
        // arrange & act
        var action = () => new ProductId(default);

        // assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_Should_SetValue()
    {
        // arrange
        const int idValue = 51;

        // act
        var productId = new ProductId(idValue);

        // assert
        Assert.Equal(idValue, productId.Value);
    }

    [Fact]
    public void Constructor_Should_SetValueToDefault_When_ValueIsNotPassed()
    {
        // arrange & act
        var productId = new ProductId();

        // assert
        Assert.Equal(default, productId.Value);
    }
}