using Reda.Domain;

namespace Reda.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private static readonly List<Order> Orders = new();
    
    public async Task<Order?> FindAsync(OrderId orderId, CancellationToken cancellationToken)
    {
        return Orders.FirstOrDefault(o => o.Id == orderId);
    }

    public void Add(Order order)
    {
        Orders.Add(order);
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
