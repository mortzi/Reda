namespace Reda.Application.Models;

public record GetOrderResponse(long OrderId, List<ProductResponse> Products, double RequiredBinWidth);