using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class TaxClassConfiguration : IEntityTypeConfiguration<TaxClass>
{
    public void Configure(EntityTypeBuilder<TaxClass> builder)
    {
        builder.HasData(
            new TaxClass
            {
                Id = new Guid("77777777-7777-7777-7777-777777777771"),
                TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                TaxRate = 19
            },
            new TaxClass
            {
                Id = new Guid("77777777-7777-7777-7777-777777777772"),
                TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                TaxRate = 7
            },
            new TaxClass
            {
                Id = new Guid("77777777-7777-7777-7777-777777777773"),
                TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                TaxRate = 0
            }
        );

        builder.Property(q => q.TaxRate)
            .IsRequired();
    }
}
