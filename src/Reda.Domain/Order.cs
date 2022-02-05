namespace Reda.Domain;

/// <summary>
/// Represents Domain Order type with Products and Package information
/// </summary>
public class Order
{
    private readonly List<Product> _products;

    /// <summary>
    /// Unique Id for Order
    /// </summary>
    public OrderId Id { get; }
    
    /// <summary>
    /// List of <see cref="Product"/> items
    /// </summary>
    public IEnumerable<Product> Products => _products.ToList();
    
    /// <summary>
    /// Package information
    /// </summary>
    public Package Package { get; }
    
    /// <summary>
    /// The width of the <see cref="Package"/> that this Order package occupies in shelf
    /// </summary>
    public Width RequiredBinWidth => Package.Width;
    
    public Order(OrderId id, IEnumerable<Product> products)
    {
        Id = id;
        _products = products.ToList();
        Package = new Package(_products);
    }
}