#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Shared.Models;

namespace maERP.Server.Configurations.Seeds
{
	public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
	{
		public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
			builder.HasData(
                new Warehouse { Id = 1, Name = "Demo Lager" }
            );
        }
	}
}