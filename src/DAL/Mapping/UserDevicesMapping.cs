using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
	public class UserDevicesMapping : IEntityTypeConfiguration<UserDevices>
	{
		public void Configure(EntityTypeBuilder<UserDevices> builder)
		{
			builder.ToTable("UserDevices");
			builder.HasKey(x => x.Id);

			builder.HasOne(ud => ud.User)
			.WithMany(u => u.UserDevices)
			.HasForeignKey(ud => ud.UserId);
		}
	}
}
