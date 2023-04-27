using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
	public class CategoryProductMapping : IEntityTypeConfiguration<CategoryProduct>
	{
		public void Configure(EntityTypeBuilder<CategoryProduct> builder)
		{
			builder.ToTable("CategoriesProducts");
			builder.HasKey(x => x.Id);

			builder.HasOne(pc => pc.Product)
					.WithMany(p => p.CategoriesProducts)
					.HasForeignKey(pc => pc.ProductId);

			builder.HasOne(pc => pc.Category)
				.WithMany(c => c.CategoriesProducts)
				.HasForeignKey(pc => pc.CategoryId);
		}
	}
}
