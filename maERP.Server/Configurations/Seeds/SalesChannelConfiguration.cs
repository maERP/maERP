#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Server.Models;
using maERP.Shared.Models;

namespace maERP.Server.Configurations.Seeds;

public class SalesChannelConfiguration : IEntityTypeConfiguration<SalesChannel>
{
	public void Configure(EntityTypeBuilder<SalesChannel> builder)
	{
		builder.HasData(
			new SalesChannel
			{
				Id = 1,
				Type = SalesChannelType.shopware5,
				Name = "Shopware Demo Shop",
				URL = "https://www.example.com/",
				Username = "demouser",
				Password = "demopass",
				ImportProducts = true,
				Warehouse = new Warehouse { Id = 1, Name = "Demo Warenlager" }
			}
		 );
    }
}