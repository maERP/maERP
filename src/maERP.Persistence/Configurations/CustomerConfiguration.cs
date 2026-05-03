using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // DateEnrollment is now DateTimeOffset and works consistently across all database providers

        // Unique constraint for CustomerId per tenant
        builder.HasIndex(x => new { x.CustomerId, x.TenantId })
            .IsUnique();

        // Filtered unique index on (TenantId, Email). Email is canonicalized to lowercase
        // by ApplicationDbContext.SaveChangesAsync, so a plain equality index suffices —
        // no functional index on LOWER(Email) (which would be provider-specific) needed.
        // The filter excludes empty/null emails so soft-deleted (merged) customers and
        // legitimately email-less rows do not block one another. ANSI-style filter so all
        // three providers (MSSQL, PostgreSQL, SQLite) accept it.
        builder.HasIndex(x => new { x.TenantId, x.Email })
            .IsUnique()
            .HasFilter("Email IS NOT NULL AND Email <> ''");

        builder.HasData(
            new Customer
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                CustomerId = 1,
                TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                Firstname = "Max",
                Lastname = "Mustermann",
                Email = "max.mustermann@maerp.de",
                CompanyName = "maERP",
                Phone = "0123456789",
                Website = "https://www.maerp.de/",
                VatNumber = "DE123456789",
                CustomerStatus = CustomerStatus.Active,
                Note = "This is a note",
                // DateEnrollment = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CustomerAddresses = new List<CustomerAddress>
                {
                    new CustomerAddress
                    {
                        Id = new Guid("22222222-2222-2222-2222-222222222221"),
                        TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                        Firstname = "Max",
                        Lastname = "Mustermann",
                        CompanyName = "maERP",
                        Street = "Musterstraße",
                        HouseNr = "1",
                        Zip = "12345",
                        City = "Musterstadt",
                        DefaultDeliveryAddress = true,
                        DefaultInvoiceAddress = true,
                        CountryId = new Guid("00000000-0000-0000-0000-000000000001") // Germany from CountryConfiguration
                    },
                    new CustomerAddress
                    {
                        Id = new Guid("22222222-2222-2222-2222-222222222222"),
                        TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                        Firstname = "Max",
                        Lastname = "Mustermann",
                        CompanyName = "maERP",
                        Street = "Musterstraße",
                        HouseNr = "2",
                        Zip = "12345",
                        City = "Musterstadt",
                        DefaultDeliveryAddress = true,
                        DefaultInvoiceAddress = true,
                        CountryId = new Guid("00000000-0000-0000-0000-000000000001") // Germany from CountryConfiguration
                    }
                }
            }
        );
    }
}