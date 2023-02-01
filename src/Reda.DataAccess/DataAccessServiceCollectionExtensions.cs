using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Reda.Domain;

namespace Reda.DataAccess;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<OrderingDbContext>((sp, builder) => builder.UseSqlServer(
            sp.GetRequiredService<IConfiguration>().GetConnectionString("SqlServer")));
        
        return services
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IProductTypeRepository, ProductTypeRepository>();
    }
}