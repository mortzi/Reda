namespace Reda.Domain;

public interface IOrderRepository
{
    Task<Order?> FindAsync(OrderId orderId, CancellationToken cancellationToken);
    void Add(Order order);
    Task SaveAsync(CancellationToken cancellationToken);
}