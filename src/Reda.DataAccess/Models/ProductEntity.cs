using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reda.DataAccess.Models;

public class ProductEntity
{
    public long Id { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public long OrderId { get; set; } = default!;
    public long ProductTypeId { get; set; } = default!;
    public ProductTypeEntity ProductType { get; set; } = default!;
}

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.HasOne(e => e.ProductType).WithMany().HasForeignKey(e => e.ProductTypeId);
        builder.Property(e => e.Quantity).IsRequired();
        builder.ToTable("Products");
    }
}