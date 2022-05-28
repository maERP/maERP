#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Data.Models;

namespace maERP.Server.Configurations.Seeds
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