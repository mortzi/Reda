using Xunit;

namespace Reda.Domain.Tests;

public class OrderIdTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsZero()
    {
        // arrange & act
        var action = () => new OrderId(0);

        // assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_Should_SetValue()
    {
        // arrange
        const long idValue = 2;

        // act
        var orderId = new OrderId(idValue);

        // assert
        Assert.Equal(idValue, orderId.Value);
    }
}