#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Shared.Models.Database;

namespace maERP.Server.Configurations.Seeds;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(
            new Country { Id = 1, Name = "Deutschland", CountryCode = "de" },
            new Country { Id = 2, Name = "Österreich", CountryCode = "at" },
            new Country { Id = 3, Name = "Schweiz", CountryCode = "ch" }
        );
    }
}
