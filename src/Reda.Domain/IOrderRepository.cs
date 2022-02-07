namespace Reda.Domain;

public interface IOrderRepository
{
    /// <summary>
    /// Find <see cref="Order"/> by <see cref="OrderId"/>
    /// </summary>
    Task<Order?> FindAsync(OrderId orderId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds new <see cref="Order"/>
    /// </summary>
    Task AddAsync(Order order, CancellationToken cancellationToken);
    
    /// <summary>
    /// Saves DbContext changes
    /// </summary>
    Task SaveAsync(CancellationToken cancellationToken);
}