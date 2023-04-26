using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.FirstName).IsRequired().HasColumnType<string>("nvarchar(50)");
            builder.Property(x => x.LastName).IsRequired().HasColumnType<string>("nvarchar(50)");
            builder.Property(x => x.Email).IsRequired().HasColumnType<string>("nvarchar(100)");
            builder.Property(x => x.Password).HasColumnType<string>("nvarchar(125)");

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(u => u.UserDevices)
			.WithOne(ud => ud.User)
			.HasForeignKey(ud => ud.UserId);
		}
    }
}
