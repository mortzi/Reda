namespace Reda.Application.Models;

public record SubmitOrderResponse(Guid OrderId, double RequiredBinWidth);