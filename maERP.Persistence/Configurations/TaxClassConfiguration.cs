using maERP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class TaxClassConfiguration : IEntityTypeConfiguration<TaxClass>
{
    public void Configure(EntityTypeBuilder<TaxClass> modelBuilder)
    {
        modelBuilder.HasData(
            new TaxClass
            {
                Id = 1,
                TaxRate = 19
            },
            new TaxClass
            {
                Id = 2,
                TaxRate = 7
            },
            new TaxClass
            {
                Id = 3,
                TaxRate = 0
            }
        );

        modelBuilder.Property(q => q.TaxRate)
            .IsRequired();
    }
}
