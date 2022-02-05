namespace Reda.Domain;

/// <summary>
/// Represents a Product item in an Order
/// </summary>
public class Product
{
    /// <summary>
    /// Quantity of the Product
    /// </summary>
    public Quantity Quantity { get; }

    /// <summary>
    /// Represents the Type of the product, such as: candle, canvas, ...
    /// </summary>
    public ProductType Type { get; }
    
    public Product(ProductType productType, Quantity quantity)
    {
        Type = productType;
        Quantity = quantity;
    }
}