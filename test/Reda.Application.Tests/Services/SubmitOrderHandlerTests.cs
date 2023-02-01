using Microsoft.Extensions.Caching.Memory;

using Moq;

using Reda.Application.Cache;
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
        const long orderId = 2;
        const string productName = "candle";

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var productType = new ProductType(7, productName, 1.2, 1);
        var productTypeRepository = new Mock<IProductTypeRepository>();
        productTypeRepository
            .Setup(r => r.FindByNameAsync(productName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productType);

        var request = new SubmitOrderRequest(
            orderId,
            new List<ProductRequest> {new(productName, 2)});

        var expected = new SubmitOrderResponse(
            orderId,
            1.2 * 2);

        var handler = new SubmitOrderHandler(
            orderRepository.Object,
            productTypeRepository.Object,
            MockMemoryCache,
            MockCacheOptions);

        // act
        var actual = await handler.Handle(request, default);

        // assert
        Assert.Equal(expected.OrderId, actual.OrderId);
        Assert.Equal(expected.RequiredBinWidth, actual.RequiredBinWidth);
        
        // make sure new Order is persisted
        orderRepository.Verify(r => r.AddAsync(It.Is<Order>(o => o.Id.Value == orderId), It.IsAny<CancellationToken>()));
        orderRepository.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public async Task Handle_ShouldThrow_WhenProductTypeNameDoesNotExist()
    {
        // arrange
        const long orderId = 2;
        const string productName = "not_existing";

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var productTypeRepository = new Mock<IProductTypeRepository>();
        productTypeRepository
            .Setup(r => r.FindByNameAsync(productName, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductType?)null);

        var request = new SubmitOrderRequest(
            orderId,
            new List<ProductRequest> {new(productName, 2)});

        var handler = new SubmitOrderHandler(
            orderRepository.Object,
            productTypeRepository.Object,
            MockMemoryCache,
            MockCacheOptions);

        // act
        var action = async () => await handler.Handle(request, default);

        // assert
        await Assert.ThrowsAsync<InvalidProductTypeException>(action);
    }
    
    [Fact]
    public async Task Handle_ShouldThrow_WhenOrderIdAlreadyExists()
    {
        // arrange
        const long orderId = 2;

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(r => r.FindAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Order(orderId, Enumerable.Empty<Product>()));
        var productTypeRepository = new Mock<IProductTypeRepository>();
        
        var request = new SubmitOrderRequest(orderId, new List<ProductRequest>());

        var handler = new SubmitOrderHandler(
            orderRepository.Object,
            productTypeRepository.Object,
            MockMemoryCache,
            MockCacheOptions);

        // act
        var action = async () => await handler.Handle(request, default);

        // assert
        await Assert.ThrowsAsync<OrderAlreadyExistsException>(action);
    }

    private static IMemoryCache MockMemoryCache => new MemoryCache(new MemoryCacheOptions());
    
    private static CacheOptions MockCacheOptions => new() { DefaultExpirationTimeSpan = TimeSpan.FromSeconds(2) };
}