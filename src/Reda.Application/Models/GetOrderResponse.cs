namespace Reda.Application.Models;

public record GetOrderResponse(Guid OrderId, List<ProductResponse> Products, double RequiredBinWidth);