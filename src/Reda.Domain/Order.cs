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
    public OrderId OrderId { get; }
    
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
    public Width RequiredWidth => Package.Width;
    
    public Order(OrderId orderId, IEnumerable<Product> products)
    {
        OrderId = orderId;
        _products = products.ToList();
        Package = new Package(_products);
    }
}