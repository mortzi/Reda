using Xunit;

namespace Reda.Domain.Tests;

public class PackageTests
{
    [Theory]
    [ClassData(typeof(PackageTestData))]
    public void Constructor_Should_CalculatePackageWidth(IEnumerable<Product> products, Width expectedWidth)
    {
        // arrange && act
        var package = new Package(products);

        // assert
        Assert.Equal(expectedWidth, package.Width);
    }
}