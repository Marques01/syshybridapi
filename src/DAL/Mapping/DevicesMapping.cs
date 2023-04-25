using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    public class DevicesMapping : IEntityTypeConfiguration<Devices>
    {
        public void Configure(EntityTypeBuilder<Devices> builder)
        {
            builder.ToTable("Devices");
            builder.HasKey(x => x.DeviceId);
            builder.Property(x => x.Mac).HasColumnType<string>("nvarchar(25)");

            builder.HasOne(d => d.User)
                .WithMany(u => u.Devices)
                .HasForeignKey(d => d.UserId);
        }
    }
}
