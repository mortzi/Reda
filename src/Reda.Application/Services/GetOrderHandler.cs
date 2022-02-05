using MediatR;

using Reda.Application.Exceptions;
using Reda.Application.Models;
using Reda.Domain;

namespace Reda.Application.Services;

/// <summary>
/// Handles <see cref="GetOrderRequest"/>
/// </summary>
public class GetOrderHandler : IRequestHandler<GetOrderRequest, GetOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
    {
        if (await _orderRepository.FindAsync(request.OrderId, cancellationToken) is not { } order)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        var productResponses = order.Products
            .Select(p => new ProductResponse(p.ProductId, p.Type.Name, p.Quantity))
            .ToList();
        
        return new GetOrderResponse(order.Id, productResponses, order.RequiredBinWidth);
    }
}