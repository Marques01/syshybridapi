﻿using BLL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.Name).IsRequired().HasColumnType<string>("nvarchar(100)");
            builder.Property(x => x.Description).HasColumnType<string>("nvarchar(125)");
            builder.Property(x => x.UnitaryValue).HasColumnType("decimal(10,2)").HasPrecision(8, 2).HasConversion<decimal>();
            builder.Property(x => x.BarCode).HasColumnType<string>("nvarchar(125)");
            builder.Property(x => x.PathImage).HasColumnType<string>("nvarchar(125)");

            builder.HasMany(x => x.Categories)
                .WithMany(x => x.Products);
        }
    }
}
