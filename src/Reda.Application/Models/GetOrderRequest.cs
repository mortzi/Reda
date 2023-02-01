using MediatR;

namespace Reda.Application.Models;

public record GetOrderRequest(long OrderId) : IRequest<GetOrderResponse>;