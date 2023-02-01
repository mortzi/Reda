namespace Reda.Domain;

/// <summary>
/// Represents a Product item in an Order
/// </summary>
public class Product
{
    /// <summary>
    /// Product unique Id
    /// </summary>
    public ProductId Id { get; }
    
    /// <summary>
    /// Quantity of the Product
    /// </summary>
    public Quantity Quantity { get; }

    /// <summary>
    /// Represents the Type of the product, such as: candle, canvas, ...
    /// </summary>
    public ProductType Type { get; }

    public Product(ProductId id, ProductType productType, Quantity quantity)
    {
        Id = id;
        Type = productType;
        Quantity = quantity;
    }

    public Product(ProductType productType, Quantity quantity)
        : this(new(), productType, quantity)
    {
    }
}