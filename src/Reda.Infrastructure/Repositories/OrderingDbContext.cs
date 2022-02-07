using Microsoft.EntityFrameworkCore;

using Reda.Infrastructure.Repositories.Models;

namespace Reda.Infrastructure.Repositories;

public class OrderingDbContext : DbContext
{
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
    }
}