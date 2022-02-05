using Xunit;

namespace Reda.Domain.Tests;

public class OrderIdTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsEmptyGuid()
    {
        // arrange & act
        var action = () => new OrderId(Guid.Empty);

        // assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_Should_SetValue()
    {
        // arrange
        var idValue = Guid.NewGuid();

        // act
        var orderId = new OrderId(idValue);

        // assert
        Assert.Equal(idValue, orderId.Value);
    }
}