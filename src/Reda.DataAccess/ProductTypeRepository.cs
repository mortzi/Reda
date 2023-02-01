using Microsoft.EntityFrameworkCore;

using Reda.DataAccess.Models;
using Reda.Domain;

namespace Reda.DataAccess;

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
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == productName, cancellationToken);

        if (productTypeEntity is null)
            return null;

        return new ProductType(
            productTypeEntity.Id,
            productTypeEntity.Name,
            productTypeEntity.Width,
            productTypeEntity.StackLimit);
    }
}