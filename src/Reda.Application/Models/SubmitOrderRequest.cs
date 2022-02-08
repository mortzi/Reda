using MediatR;

namespace Reda.Application.Models;

public record SubmitOrderRequest(Guid OrderId, List<ProductRequest> Products) : IRequest<SubmitOrderResponse>;
