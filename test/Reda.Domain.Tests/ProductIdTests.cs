using Xunit;

namespace Reda.Domain.Tests;

public class ProductIdTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsEmptyGuid()
    {
        // arrange & act
        var action = () => new ProductId(Guid.Empty);

        // assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_Should_SetValue()
    {
        // arrange
        var idValue = Guid.NewGuid();

        // act
        var productId = new ProductId(idValue);

        // assert
        Assert.Equal(idValue, productId.Value);
    }
}