namespace Reda.Domain;

/// <summary>
/// Represents the Type of the product, such as: candle, canvas, ...
/// </summary>
/// <param name="Name">Product Name</param>
/// <param name="Width">The width that one item of this product occupies</param>
/// <param name="StackLimit">Represents the number that this product can stack upon each other without taking more <see cref="Width"/></param>
public record ProductType(string Name, Width Width, Quantity StackLimit);