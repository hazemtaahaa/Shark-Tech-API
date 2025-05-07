using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
        builder.Property(c => c.Description).HasMaxLength(700);
        //builder.HasMany(c => c.Products)
        //    .WithOne(p => p.Category)
        //    .HasForeignKey(p => p.CategoryId);

        builder.HasData(
           new Category { Id = Guid.NewGuid(), Name = "Electronics", Description = "Devices and gadgets" }
            );

    }
}

