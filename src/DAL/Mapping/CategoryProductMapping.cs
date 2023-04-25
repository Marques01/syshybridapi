using BLL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Mapping
{
    public class CategoryProductMapping : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.ToTable("CategoriesProducts");

            builder.HasKey(x => new { x.CategoryId, x.ProductId });

            builder.HasOne(x => x.Product)
                .WithMany(x => x.CategoriesProducts)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.CategoriesProducts)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
