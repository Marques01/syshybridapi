using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
	public class PurchaseProductMapping : IEntityTypeConfiguration<PurchaseProduct>
	{
		public void Configure(EntityTypeBuilder<PurchaseProduct> builder)
		{
			builder.ToTable("PurchaseProducts");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.UnitaryValue).HasColumnType("decimal(10,2)").HasPrecision(8, 2).HasConversion<decimal>();

			builder.HasOne(u => u.User)
				.WithMany(purchaseProduct => purchaseProduct.PurchaseProducts)
				.HasForeignKey(u => u.UserId);

			builder.HasOne(product => product.Product)
				.WithMany(purchaseProduct => purchaseProduct.PurchaseProducts)
				.HasForeignKey(x => x.ProductId);

			builder.HasOne(x => x.Purchase)
				.WithMany(purchaseProduct => purchaseProduct.PurchaseProducts)
				.HasForeignKey(x => x.PurchaseId);
		}
	}
}
