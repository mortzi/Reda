namespace Reda.Domain;

/// <summary>
/// Represents a part in a package where items have the same <see cref="ProductType"/> and possibly can stack up
/// </summary>
public class PackagePart
{
    /// <summary>
    /// Product type
    /// </summary>
    public ProductType ProductType { get; }
    
    /// <summary>
    /// The number of <see cref="Product"/>s in this package
    /// </summary>
    public Quantity Quantity { get; }
    
    /// <summary>
    /// The width that this package will take when placed in a shelf
    /// </summary>
    public Width Width { get; }

    public PackagePart(ProductType productType, Quantity quantity)
    {
        ProductType = productType;
        Quantity = quantity;

        Width = Math.Ceiling((double)quantity / productType.StackLimit) * productType.Width;
    }
}