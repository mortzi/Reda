using MediatR;

namespace Reda.Application.Models;

public record SubmitOrderRequest : IRequest<SubmitOrderResponse>
{
    public Guid OrderId { get; set; } = default!;
    public List<ProductRequest> Products { get; set; } = new();
}