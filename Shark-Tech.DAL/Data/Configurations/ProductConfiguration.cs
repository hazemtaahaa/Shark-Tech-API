using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shark_Tech.DAL;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(80);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);

        builder.Property(p => p.Quantity).IsRequired();

        // Fix: Use HasPrecision instead of HasColumnType for decimal properties
        // 
        builder.Property(p => p.Price).IsRequired().HasPrecision(18, 2);

        builder.HasMany(p => p.ProductImages)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);

        builder.HasData( new Product
        {
            Id = Guid.NewGuid(),
            Name = "Sample Product",
            Description = "This is a sample product description.",
            Price = 99.99m,
            Quantity = 10,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });
    }
}
