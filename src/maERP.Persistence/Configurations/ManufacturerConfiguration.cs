using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
{
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.Street)
            .HasMaxLength(255);
            
        builder.Property(m => m.City)
            .HasMaxLength(255);
            
        builder.Property(m => m.State)
            .HasMaxLength(255);
            
        builder.Property(m => m.Country)
            .HasMaxLength(255);
            
        builder.Property(m => m.ZipCode)
            .HasMaxLength(20);
            
        builder.Property(m => m.Phone)
            .HasMaxLength(50);
            
        builder.Property(m => m.Email)
            .HasMaxLength(255);
            
        builder.Property(m => m.Website)
            .HasMaxLength(500);
            
        builder.Property(m => m.Logo)
            .HasMaxLength(500);

        // Add some seed data
        builder.HasData(
            new Manufacturer
            {
                Id = 1,
                Name = "Beispiel Hersteller GmbH",
                Street = "Musterstraße 123",
                City = "Berlin",
                State = "Berlin",
                Country = "Deutschland",
                ZipCode = "10115",
                Phone = "+49 30 12345678",
                Email = "info@beispiel-hersteller.de",
                Website = "https://www.beispiel-hersteller.de"
            }
        );
    }
}