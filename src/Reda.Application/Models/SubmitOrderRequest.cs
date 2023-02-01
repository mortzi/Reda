using MediatR;

namespace Reda.Application.Models;

public record SubmitOrderRequest(long OrderId, List<ProductRequest> Products) : IRequest<SubmitOrderResponse>;
