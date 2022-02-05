using Xunit;

namespace Reda.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void Constructor_Should_SetOrderId()
    {
        // arrange
        var orderId = new OrderId(Guid.NewGuid());

        // act
        var order = new Order(orderId, new List<Product>());

        // assert
        Assert.Equal(orderId, order.Id);
        Assert.Empty(order.Products);
    }
    
    [Fact]
    public void Constructor_Should_SetProducts()
    {
        // arrange
        var orderId = new OrderId(Guid.NewGuid());
        
        var products = new List<Product>
        {
            new(PackageTestData.CalendarType, quantity: 2),
            new(PackageTestData.MugType, quantity: 3)
        };

        // act
        var order = new Order(orderId, products);

        // assert
        Assert.Collection(
            order.Products,
            p =>
            {
                Assert.Equal(PackageTestData.CalendarType, p.Type);
                Assert.Equal(2, p.Quantity.Value);
            },
            p =>
            {
                Assert.Equal(PackageTestData.MugType, p.Type);
                Assert.Equal(3, p.Quantity.Value);
            });
    }
    
    [Fact]
    public void Constructor_ShouldNot_ChangeProductGrouping()
    {
        // arrange
        var orderId = new OrderId(Guid.NewGuid());
        
        var products = new List<Product>
        {
            new(PackageTestData.MugType, quantity: 1),
            new(PackageTestData.CalendarType, quantity: 2),
            new(PackageTestData.MugType, quantity: 3)
        };

        // act
        var order = new Order(orderId, products);

        // assert
        Assert.Collection(
            order.Products,
            p =>
            {
                Assert.Equal(PackageTestData.MugType, p.Type);
                Assert.Equal(1, p.Quantity.Value);
            },
            p =>
            {
                Assert.Equal(PackageTestData.CalendarType, p.Type);
                Assert.Equal(2, p.Quantity.Value);
            },
            p =>
            {
                Assert.Equal(PackageTestData.MugType, p.Type);
                Assert.Equal(3, p.Quantity.Value);
            });
    }
    
    [Fact]
    public void Constructor_Should_SetRequiredBinWidth()
    {
        // arrange
        var products = new[]
        {
            new Product(PackageTestData.CardsType, 2),
            new Product(PackageTestData.MugType, 2)
        };
        
        Width expectedWidth = 2 * PackageTestData.CardsType.Width + 1 * PackageTestData.MugType.Width;
        var orderId = new OrderId(Guid.NewGuid());

        // act
        var order = new Order(orderId, products);

        // assert
        Assert.Equal(expectedWidth, order.Package.Width);
        Assert.Equal(expectedWidth, order.RequiredBinWidth);
    }
}