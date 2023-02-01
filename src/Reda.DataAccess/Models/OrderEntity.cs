using System.Collections.ObjectModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reda.DataAccess.Models;

public class OrderEntity
{
    public long Id { get; set; } = default!;
    public Collection<ProductEntity> Products { get; set; } = new();
}

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.HasMany(e => e.Products).WithOne().HasForeignKey(e => e.OrderId);
        builder.ToTable("Orders");
    }
}