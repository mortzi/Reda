namespace Reda.Application.Models;

public record ProductRequest
{
    /// <summary>
    /// Product name
    /// </summary>
    public string ProductType { get; set; } = default!;

    /// <summary>
    /// Product quantity
    /// </summary>
    public int Quantity { get; set; } = default!;
}