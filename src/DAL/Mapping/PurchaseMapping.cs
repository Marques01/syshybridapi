using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
	public class PurchaseMapping : IEntityTypeConfiguration<Purchase>
	{
		public void Configure(EntityTypeBuilder<Purchase> builder)
		{
			builder.ToTable("Purchases");
			builder.HasKey(x => x.PurchaseId);
			builder.Property(x => x.Status).HasColumnType<string>("nvarchar(25)");
			builder.Property(x => x.Invoice).HasColumnType<string>("nvarchar(50)");

			builder.HasOne(supplier => supplier.Supplier)
				.WithMany(purchases => purchases.Purchases)
				.HasForeignKey(supplier => supplier.SupplierId);
		}
	}
}
