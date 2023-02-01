using System.Runtime.CompilerServices;

using MediatR;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Reda.Application.Cache;
using Reda.Application.Exceptions;
using Reda.Application.Models;
using Reda.Domain;

namespace Reda.Application.Services;

/// <summary>
/// Handles <see cref="SubmitOrderRequest"/>
/// </summary>
public class SubmitOrderHandler : IRequestHandler<SubmitOrderRequest, SubmitOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMemoryCache _productTypeCache;
    private readonly CacheOptions _cacheOptions;

    public SubmitOrderHandler(
        IOrderRepository orderRepository,
        IProductTypeRepository productTypeRepository,
        IMemoryCache productTypeCache,
        IOptions<CacheOptions> cacheOptions)
    {
        _orderRepository = orderRepository;
        _productTypeRepository = productTypeRepository;
        _productTypeCache = productTypeCache;
        _cacheOptions = cacheOptions.Value;
    }
    
    public async Task<SubmitOrderResponse> Handle(SubmitOrderRequest request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        
        await ThrowIfOrderAlreadyExists(orderId, cancellationToken);

        var products = await CreateProducts(request.Products, cancellationToken).ToListAsync(cancellationToken);

        var order = new Order(orderId, products);

        await _orderRepository.AddAsync(order, cancellationToken);
        await _orderRepository.SaveAsync(cancellationToken);

        return new SubmitOrderResponse(order.Id, order.RequiredBinWidth);
    }

    private async IAsyncEnumerable<Product> CreateProducts(
        IEnumerable<ProductRequest> productRequests,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var p in productRequests)
        {
            var productType = await GetProductType(p.ProductType, cancellationToken);
            yield return new Product(productType, p.Quantity);
        }
    }

    private async Task<ProductType> GetProductType(string productName, CancellationToken cancellationToken)
    {
        var productType = await _productTypeCache.GetOrCreateAsync(
            productName,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheOptions.DefaultExpirationTimeSpan;
                return await _productTypeRepository.FindByNameAsync(productName, cancellationToken);
            });
        
        return productType ?? throw new InvalidProductTypeException(productName);
    }

    private async Task ThrowIfOrderAlreadyExists(OrderId orderId, CancellationToken cancellationToken)
    {
        if (await _orderRepository.FindAsync(orderId, cancellationToken) is not null)
        {
            throw new OrderAlreadyExistsException(orderId);
        }
    }
}