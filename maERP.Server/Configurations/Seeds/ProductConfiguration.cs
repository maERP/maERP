#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Shared.Models.Database;

namespace maERP.Server.Configurations.Seeds;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = 1,
                Sku = "1001",
                Name = "Testprodukt 1",
                Description = "Beschreibung 1",
                Price = 100,
                TaxClass = new TaxClass { Id = 1, TaxRate = 0, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
                DateModified = DateTime.Now,
                DateCreated = DateTime.Now
            }
        );
    }
}