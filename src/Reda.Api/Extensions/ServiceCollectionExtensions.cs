using Reda.Domain;
using Reda.Infrastructure.Repositories;

namespace Reda.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IProductTypeRepository, ProductTypeRepository>();
    }
}