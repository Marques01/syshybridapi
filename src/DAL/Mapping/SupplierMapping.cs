using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
	public class SupplierMapping : IEntityTypeConfiguration<Supplier>
	{
		public void Configure(EntityTypeBuilder<Supplier> builder)
		{
			builder.ToTable("Suppliers");
			builder.HasKey(x => x.SupplierId);
			builder.Property(x => x.Adress).HasColumnType<string>("nvarchar(125)");
			builder.Property(x => x.Name).HasColumnType<string>("nvarchar(75)");
			builder.Property(x => x.CNPJ).HasColumnType<string>("nvarchar(30)");
			builder.Property(x => x.CorporateName).HasColumnType<string>("nvarchar(75)");
			builder.Property(x => x.Email).HasColumnType<string>("nvarchar(75)");
			builder.Property(x => x.PhoneNumber).HasColumnType<string>("nvarchar(25)");			
		}
	}
}
