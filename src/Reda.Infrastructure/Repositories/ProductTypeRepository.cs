using Reda.Domain;

namespace Reda.Infrastructure.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private static readonly List<ProductType> ProductTypes = new List<ProductType>
    {
        new("mug", 94, 4),
        new("calendar", 14, 1),
        new("canvas", 20, 1),
        new("cards", 4.7, 1)
    };
    
    public async Task<ProductType?> FindByNameAsync(string productName, CancellationToken cancellationToken)
    {
        return ProductTypes.FirstOrDefault(p => p.Name == productName);
    }
}