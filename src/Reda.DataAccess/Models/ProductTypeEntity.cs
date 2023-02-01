using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reda.DataAccess.Models;

public class ProductTypeEntity
{
    public long Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int StackLimit { get; set; } = default!;
    public double Width { get; set; } = default!;
}

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductTypeEntity>
{
    public void Configure(EntityTypeBuilder<ProductTypeEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name).IsUnique();
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.StackLimit).IsRequired();
        builder.Property(e => e.Width).IsRequired();
        builder.ToTable("ProductTypes");
        
        builder.HasData(
            new List<ProductTypeEntity>
            {
                new()
                {
                    Id = 1,
                    Name = "photoBook",
                    Width = 19,
                    StackLimit = 1
                },
                new()
                {
                    Id = 2,
                    Name = "calendar",
                    Width = 10,
                    StackLimit = 1
                },
                new()
                {
                    Id = 3,
                    Name = "canvas",
                    Width = 16,
                    StackLimit = 1
                },
                new()
                {
                    Id = 4,
                    Name = "cards",
                    Width = 4.7,
                    StackLimit = 1
                },
                new()
                {
                    Id = 5,
                    Name = "mug",
                    Width = 94,
                    StackLimit = 4
                },
            });
    }
}