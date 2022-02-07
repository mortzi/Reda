using System.Runtime.CompilerServices;

using MediatR;

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

    public SubmitOrderHandler(
        IOrderRepository orderRepository,
        IProductTypeRepository productTypeRepository)
    {
        _orderRepository = orderRepository;
        _productTypeRepository = productTypeRepository;
    }
    
    public async Task<SubmitOrderResponse> Handle(SubmitOrderRequest request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        
        await ThrowIfOrderAlreadyExists(orderId, cancellationToken);

        var products = await MapProductsAsync(request.Products, cancellationToken).ToListAsync(cancellationToken);

        var order = new Order(orderId, products);

        await _orderRepository.AddAsync(order, cancellationToken);
        await _orderRepository.SaveAsync(cancellationToken);

        return new SubmitOrderResponse(order.Id, order.RequiredBinWidth);
    }

    private async IAsyncEnumerable<Product> MapProductsAsync(
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
        return await _productTypeRepository.FindByNameAsync(productName, cancellationToken)
               ?? throw new InvalidProductTypeException(productName);
    }

    private async Task ThrowIfOrderAlreadyExists(OrderId orderId, CancellationToken cancellationToken)
    {
        if (await _orderRepository.FindAsync(orderId, cancellationToken) is not null)
        {
            throw new OrderAlreadyExistsException(orderId);
        }
    }
}