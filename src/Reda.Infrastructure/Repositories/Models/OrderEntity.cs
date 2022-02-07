using System.Collections.ObjectModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reda.Infrastructure.Repositories.Models;

public class OrderEntity
{
    public Guid Id { get; set; } = default!;
    public Collection<ProductEntity> Products { get; set; } = new();
}

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsFixedLength().ValueGeneratedNever();
        builder.HasMany(e => e.Products).WithOne(p => p.Order);
        builder.ToTable("Orders");
    }
}