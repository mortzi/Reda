using System.Collections.ObjectModel;

using Microsoft.EntityFrameworkCore;

using Reda.Domain;
using Reda.Infrastructure.Repositories.Models;

namespace Reda.Infrastructure.Repositories;

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
            .Include(o => o.Products)
            .ThenInclude(p => p.ProductType)
            .FirstOrDefaultAsync(o => o.Id == orderId.Value, cancellationToken);

        if (order is null)
            return null;

        // map products
        var products = order.Products
            .Select(p =>
                new Product(
                    p.Id,
                    new ProductType(p.ProductType.Name, p.ProductType.Width, p.ProductType.StackLimit),
                    p.Quantity));

        return new Order(order.Id, products);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        var products = new Collection<ProductEntity>();

        // use as cache for multiple products with same type
        var nameProductTypeMap = new Dictionary<string, ProductTypeEntity>();
        
        foreach (var p in order.Products)
        {
            var typeName = p.Type.Name;
            
            // get or add product type
            if (nameProductTypeMap.GetValueOrDefault(typeName) is not { } productType)
            {
                productType = await _dbContext.Set<ProductTypeEntity>()
                    .FirstAsync(type => type.Name == typeName, cancellationToken);

                nameProductTypeMap.Add(typeName, productType);
            }

            products.Add(new ProductEntity {Id = p.Id, ProductType = productType, Quantity = p.Quantity});
        }

        var orderEntity = new OrderEntity
        {
            Id = order.Id,
            Products = products
        };

        _dbContext.Set<OrderEntity>().Add(orderEntity);
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
