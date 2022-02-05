using Moq;

using Reda.Application.Exceptions;
using Reda.Application.Models;
using Reda.Application.Services;
using Reda.Domain;

using Xunit;

namespace Reda.Application.Tests.Services;

public class SubmitOrderHandlerTests
{
    [Fact]
    public async Task Handle_Should_SubmitOrder()
    {
        // arrange
        var orderId = Guid.NewGuid();
        const string productName = "candle";

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var productType = new ProductType(productName, 1.2, 1);
        var productTypeRepository = new Mock<IProductTypeRepository>();
        productTypeRepository
            .Setup(r => r.FindByNameAsync(productName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productType);
        
        var request = new SubmitOrderRequest
        {
            OrderId = orderId,
            Products = new List<ProductRequest>
            {
                new()
                {
                    ProductType = productName,
                    Quantity = 2
                }
            }
        };

        var expected = new SubmitOrderResponse(
            orderId,
            1.2 * 2);

        var handler = new SubmitOrderHandler(orderRepository.Object, productTypeRepository.Object);

        // act
        var actual = await handler.Handle(request, default);

        // assert
        Assert.Equal(expected.OrderId, actual.OrderId);
        Assert.Equal(expected.RequiredBinWidth, actual.RequiredBinWidth);
        
        // make sure new Order is persisted
        orderRepository.Verify(r => r.Add(It.Is<Order>(o => o.Id.Value == orderId)));
        orderRepository.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public async Task Handle_ShouldThrow_WhenOrderIdAlreadyExists()
    {
        // arrange
        var orderId = Guid.NewGuid();

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Order(orderId, Enumerable.Empty<Product>()));
        var productTypeRepository = new Mock<IProductTypeRepository>();
        
        var request = new SubmitOrderRequest {OrderId = orderId};

        var handler = new SubmitOrderHandler(orderRepository.Object, productTypeRepository.Object);

        // act
        var action = async () => await handler.Handle(request, default);

        // assert
        await Assert.ThrowsAsync<OrderAlreadyExistsException>(action);
    }
}