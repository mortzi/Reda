using System.Collections.ObjectModel;

using Microsoft.EntityFrameworkCore;

using Reda.DataAccess.Models;
using Reda.Domain;

namespace Reda.DataAccess;

public class OrderRepository : IOrderRepository
{
    private readonly OrderingDbContext _dbContext;

    public OrderRepository(OrderingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Order?> FindAsync(OrderId orderId, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Set<OrderEntity>()
            .AsNoTracking()
            .Include(o => o.Products)
            .ThenInclude(p => p.ProductType)
            .FirstOrDefaultAsync(o => o.Id == orderId.Value, cancellationToken);

        if (order is null)
            return null;

        var products = order.Products.Select(p =>
            new Product(
                new ProductType(p.ProductType.Id, p.ProductType.Name, p.ProductType.Width, p.ProductType.StackLimit),
                p.Quantity));

        return new Order(order.Id, products);
    }

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        var orderEntity = new OrderEntity
        {
            Id = order.Id,
            Products = new Collection<ProductEntity>(order.Products.Select(p => new ProductEntity
            {
                Quantity = p.Quantity,
                ProductTypeId = p.Type.Id,
                OrderId = order.Id
            }).ToArray())
        };

        _dbContext.Set<OrderEntity>().Add(orderEntity);

        return Task.CompletedTask;
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
