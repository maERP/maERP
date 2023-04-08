#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Shared.Models;

namespace maERP.Server.Configurations.Seeds;

public class TaxClassConfiguration : IEntityTypeConfiguration<TaxClass>
{
	public void Configure(EntityTypeBuilder<TaxClass> builder)
    {
	    builder.HasData(
            new TaxClass { TaxClassId = 1, TaxRate = 0 },
            new TaxClass { TaxClassId = 2, TaxRate = 7 },
            new TaxClass { TaxClassId = 3, TaxRate = 19 }
        );
    }
}