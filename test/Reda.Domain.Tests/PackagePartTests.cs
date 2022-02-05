using Xunit;

namespace Reda.Domain.Tests;

public class PackagePartTests
{
    [Fact]
    public void Constructor_Should_CalculateWidth()
    {
        // arrange
        
        // act
        var packagePart = new PackagePart(PackageTestData.CanvasType, 4);
        
        // assert
        Assert.Equal(4, packagePart.Quantity.Value);
        Assert.Equal(PackageTestData.CanvasType, packagePart.ProductType);
        Assert.Equal(PackageTestData.CanvasType.Width * 4, packagePart.Width.Value);
    }
    
    [Fact]
    public void Constructor_Should_CalculateWidthForStackLimit()
    {
        // arrange
        
        // act
        var packagePart = new PackagePart(PackageTestData.MugType, 5);
        
        // assert
        Assert.Equal(5, packagePart.Quantity.Value);
        Assert.Equal(PackageTestData.MugType, packagePart.ProductType);
        Assert.Equal(PackageTestData.MugType.Width * 2, packagePart.Width.Value);
    }
}