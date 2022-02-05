namespace Reda.Application.Models;

public record ProductRequest
{
    /// <summary>
    /// Product name
    /// </summary>
    /// <example>canvas</example>
    /// <example>mug</example>
    public string ProductType { get; set; } = default!;

    /// <summary>
    /// Product quantity
    /// </summary>
    /// <example>2</example>
    public int Quantity { get; set; } = default!;
}