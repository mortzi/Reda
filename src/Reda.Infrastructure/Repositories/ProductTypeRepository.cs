using Microsoft.EntityFrameworkCore;

using Reda.Domain;
using Reda.Infrastructure.Repositories.Models;

namespace Reda.Infrastructure.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly OrderingDbContext _dbContext;

    public ProductTypeRepository(OrderingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ProductType?> FindByNameAsync(string productName, CancellationToken cancellationToken)
    {
        var productTypeEntity = await _dbContext
            .Set<ProductTypeEntity>()
            .FirstOrDefaultAsync(p => p.Name == productName, cancellationToken);

        if (productTypeEntity is null)
            return null;

        return new ProductType(productTypeEntity.Name, productTypeEntity.Width, productTypeEntity.StackLimit);
    }
}