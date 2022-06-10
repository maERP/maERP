#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using maERP.Data.Models;

namespace maERP.Server.Configurations.Seeds
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    SKU = "1001",
                    Name = "Testprodukt 1",
                    Description = "Beschreibung 1",
                    Price = 100,
                    TaxClassId = 3,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    SKU = "1002",
                    Name = "Testprodukt 2",
                    Description = "Beschreibung 2",
                    Price = 100,
                    TaxClassId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 3,
                    SKU = "1003",
                    Name = "Testprodukt 3",
                    Description = "Beschreibung 3",
                    Price = 100,
                    TaxClassId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
}