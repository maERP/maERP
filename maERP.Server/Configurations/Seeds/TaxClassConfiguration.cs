#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Shared.Models;

namespace maERP.Server.Configurations.Seeds;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
	public void Configure(EntityTypeBuilder<Country> builder)
    {
	    builder.HasData(
            new Country { CountryId = 1, Name = "Deutschland", CountryCode = "de" },
            new Country { CountryId = 2, Name = "Österreich", CountryCode = "at" },
            new Country { CountryId = 3, Name = "Schweiz", CountryCode = "ch" }
        );
    }
}