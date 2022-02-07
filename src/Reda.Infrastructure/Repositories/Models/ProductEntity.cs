using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reda.Infrastructure.Repositories.Models;

public class ProductEntity
{
    public Guid Id { get; set; } = default!;
    public ProductTypeEntity ProductType { get; set; } = default!;
    public OrderEntity Order { get; set; } = default!;
    public int Quantity { get; set; } = default!;
}

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsFixedLength().ValueGeneratedOnAdd();
        builder.HasOne(e => e.Order).WithMany(o => o.Products);
        builder.HasOne(e => e.ProductType);
        builder.Property(e => e.Quantity).IsRequired();
        builder.ToTable("Products");
    }
}