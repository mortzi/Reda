namespace Reda.Application.Models;

/// <summary>
/// Product request
/// </summary>
/// <param name="ProductType">Product name</param>
/// <param name="Quantity">Product quantity</param>
public record ProductRequest(string ProductType, int Quantity);
