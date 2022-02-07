using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reda.Infrastructure.Repositories.Models;

public class ProductTypeEntity
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int StackLimit { get; set; } = default!;
    public double Width { get; set; } = default!;
}

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductTypeEntity>
{
    public void Configure(EntityTypeBuilder<ProductTypeEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsFixedLength();
        builder.HasIndex(e => e.Name).IsUnique();
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Width).IsRequired();
        builder.Property(e => e.StackLimit).IsRequired();
        builder.ToTable("ProductTypes");
        
        builder.HasData(
            new List<ProductTypeEntity>
            {
                new()
                {
                    Id = new Guid("cbffff55-25c1-4e39-abac-20dfda442c27"),
                    Name = "photoBook",
                    Width = 19,
                    StackLimit = 1
                },
                new()
                {
                    Id = new Guid("a2b6fb96-7e3c-44e6-905c-72ce8bebc71c"),
                    Name = "calendar",
                    Width = 10,
                    StackLimit = 1
                },
                new()
                {
                    Id = new Guid("a9bf52be-3bf1-46f2-b0bb-a8f8ff02090e"),
                    Name = "canvas",
                    Width = 16,
                    StackLimit = 1
                },
                new()
                {
                    Id = new Guid("557ade17-e9a9-48e8-b5f9-7010c7db6514"),
                    Name = "cards",
                    Width = 4.7,
                    StackLimit = 1
                },
                new()
                {
                    Id = new Guid("4d6f3b85-cf39-4aae-8a3d-3b0337e37fa9"),
                    Name = "mug",
                    Width = 94,
                    StackLimit = 4
                },
            });
    }
}