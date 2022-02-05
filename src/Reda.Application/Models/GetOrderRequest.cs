using MediatR;

namespace Reda.Application.Models;

public record GetOrderRequest : IRequest<GetOrderResponse>
{
    public Guid OrderId { get; set; } = default!;
}