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

        builder.HasData(
            new Customer
            {
                Id = 1,
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
                        Id = 1,
                        Firstname = "Max",
                        Lastname = "Mustermann",
                        CompanyName = "maERP",
                        Street = "Musterstraße",
                        HouseNr = "1",
                        Zip = "12345",
                        City = "Musterstadt",
                        DefaultDeliveryAddress = true,
                        DefaultInvoiceAddress = true,
                        CountryId = 1
                    },
                    new CustomerAddress
                    {
                        Id = 2,
                        Firstname = "Max",
                        Lastname = "Mustermann",
                        CompanyName = "maERP",
                        Street = "Musterstraße",
                        HouseNr = "2",
                        Zip = "12345",
                        City = "Musterstadt",
                        DefaultDeliveryAddress = true,
                        DefaultInvoiceAddress = true,
                        CountryId = 1
                    }
                }
            }
        );
    }
}