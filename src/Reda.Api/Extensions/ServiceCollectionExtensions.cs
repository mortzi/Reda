using Microsoft.EntityFrameworkCore;

using Reda.Domain;
using Reda.Infrastructure.Repositories;

namespace Reda.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<OrderingDbContext>((sp, b) => b.UseSqlServer(
            sp.GetRequiredService<IConfiguration>().GetConnectionString("SqlServer")));
        
        return services
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IProductTypeRepository, ProductTypeRepository>();
    }
}