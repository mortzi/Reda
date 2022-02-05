namespace Reda.Domain;

/// <summary>
/// Represents a Product item in an Order
/// </summary>
public class Product
{
    /// <summary>
    /// Product unique Id
    /// </summary>
    public ProductId ProductId { get; }
    
    /// <summary>
    /// Quantity of the Product
    /// </summary>
    public Quantity Quantity { get; }

    /// <summary>
    /// Represents the Type of the product, such as: candle, canvas, ...
    /// </summary>
    public ProductType Type { get; }

    public Product(ProductType productType, Quantity quantity)
        : this(new ProductId(Guid.NewGuid()), productType, quantity)
    {
    }
    
    public Product(ProductId productId, ProductType productType, Quantity quantity)
    {
        ProductId = productId;
        Type = productType;
        Quantity = quantity;
    }
}