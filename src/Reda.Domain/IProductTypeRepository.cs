namespace Reda.Domain;

public interface IProductTypeRepository
{
    Task<ProductType?> FindByNameAsync(string productName, CancellationToken cancellationToken);
}