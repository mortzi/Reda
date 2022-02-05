namespace Reda.Domain;

/// <summary>
/// Represents the placement structure of an Order in a shelf
/// It can have zero or more <see cref="PackagePart"/>s 
/// </summary>
public class Package
{
    /// <summary>
    /// Sum of all <see cref="PackageParts"/> widths
    /// </summary>
    public Width Width { get; }
    
    /// <summary>
    /// The package parts with same <see cref="ProductType"/>
    /// </summary>
    public IReadOnlyList<PackagePart> PackageParts { get; }

    public Package(IEnumerable<Product> products)
    {
        PackageParts = CreateParts(products);
        Width = PackageParts.Sum(s => s.Width);
    }

    private IReadOnlyList<PackagePart> CreateParts(IEnumerable<Product> products)
    {
        return products
            .GroupBy(p => p.Type)
            .Select(grouping => new PackagePart(grouping.Key, grouping.Sum(p => p.Quantity)))
            .ToList();
    }
}