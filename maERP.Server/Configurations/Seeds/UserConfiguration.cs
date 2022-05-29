﻿#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Configurations.Seeds
{
	public class UserConfiguration : IEntityTypeConfiguration<ApiUser>
	{
		public void Configure(EntityTypeBuilder<ApiUser> builder)
		{
			var user = new ApiUser
			{
				FirstName = "Admin",
				LastName = "Admin",
				Email = "admin@localhost.com",
			};

			var password = new PasswordHasher<ApiUser>();
			var hashed = password.HashPassword(user, "admin!admin");
			user.PasswordHash = hashed;


			builder.HasData(user);
		}
	}
}