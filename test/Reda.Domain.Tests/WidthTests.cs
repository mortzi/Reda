using Xunit;

namespace Reda.Domain.Tests;

public class WidthTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsLessThanZero()
    {
        // arrange & act
        var action = () => new Width(-1);

        // assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_Should_SetValue()
    {
        // arrange
        var value = 1.9d;

        // act
        var width = new Width(value);

        // assert
        Assert.Equal(value, width.Value);
    }
    
    [Fact]
    public void Constructor_Should_SetZeroValue()
    {
        // arrange
        var value = 0;

        // act
        var width = new Width(value);

        // assert
        Assert.Equal(value, width.Value);
    }
}