using Xunit;

namespace Reda.Domain.Tests;

public class QuantityTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsLessThanZero()
    {
        // arrange & act
        var action = () => new Quantity(-1);

        // assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_Should_SetValue()
    {
        // arrange
        var value = 2;

        // act
        var quantity = new Quantity(value);

        // assert
        Assert.Equal(value, quantity.Value);
    }
    
    [Fact]
    public void Constructor_Should_SetZeroValue()
    {
        // arrange
        var value = 0;

        // act
        var quantity = new Quantity(value);

        // assert
        Assert.Equal(value, quantity.Value);
    }
}