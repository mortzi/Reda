using MediatR;

namespace Reda.Application.Models;

public record GetOrderRequest(Guid OrderId) : IRequest<GetOrderResponse>;