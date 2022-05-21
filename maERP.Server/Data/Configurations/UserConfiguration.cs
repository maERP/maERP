#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Server.Data.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<ApiUser>
	{
		public void Configure(EntityTypeBuilder<ApiUser> builder)
		{
			builder.HasData(
				new ApiUser
				{
					FirstName = "Admin",
					LastName = "Admin",
					Email = "admin@localhost.com",
				}
			);
		}
	}
}