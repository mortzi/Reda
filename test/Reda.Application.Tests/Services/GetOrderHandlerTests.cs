using Moq;

using Reda.Application.Exceptions;
using Reda.Application.Models;
using Reda.Application.Services;
using Reda.Domain;

using Xunit;

namespace Reda.Application.Tests.Services;

public class GetOrderHandlerTests
{
    [Fact]
    public async Task Handle_Should_GetOrderByItsId()
    {
        // arrange
        const long orderId = 3;

        var order = new Order(
            orderId,
            new List<Product> {new(new ProductType(3, "candle", 1.2, 1), 2)});
        
        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        
        var request = new GetOrderRequest(orderId);

        var expected = new GetOrderResponse(
            orderId,
            new List<ProductResponse> {new("candle", 2)},
            1.2 * 2);

        var handler = new GetOrderHandler(orderRepository.Object);

        // act
        var actual = await handler.Handle(request, default);

        // assert
        Assert.Equal(expected.OrderId, actual.OrderId);
        Assert.Equal(expected.RequiredBinWidth, actual.RequiredBinWidth);
        
        // do not compare product ids
        Assert.Equal(
            expected.Products.Select(p => new {p.Quantity, p.ProductType}),
            actual.Products.Select(p => new {p.Quantity, p.ProductType}));
    }
    
    [Fact]
    public async Task Handle_ShouldThrow_WhenOrderIdDoesNotExist()
    {
        // arrange
        const long orderId = 3;


        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);
        
        var request = new GetOrderRequest(orderId);

        var handler = new GetOrderHandler(orderRepository.Object);

        // act
        var action = async () => await handler.Handle(request, default);

        // assert
        await Assert.ThrowsAsync<OrderNotFoundException>(action);
    }
}