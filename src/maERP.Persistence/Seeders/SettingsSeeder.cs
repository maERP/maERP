using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Seeders;

public static class SettingsSeeder
{
    public static void SeedSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Setting>().HasData(
            new Setting { Id = 1, Key = "Company.Name", Value = "Musterfirma GmbH" },
            new Setting { Id = 2, Key = "Company.Address", Value = "Musterstraße 123" },
            new Setting { Id = 3, Key = "Company.ZipCity", Value = "12345 Musterstadt" },
            new Setting { Id = 4, Key = "Company.Country", Value = "Deutschland" },
            new Setting { Id = 5, Key = "Company.Phone", Value = "+49 123 456789" },
            new Setting { Id = 6, Key = "Company.Email", Value = "info@musterfirma.de" },
            new Setting { Id = 7, Key = "Company.Website", Value = "www.musterfirma.de" },
            new Setting { Id = 8, Key = "Company.TaxId", Value = "123/456/7890" },
            new Setting { Id = 9, Key = "Company.VatId", Value = "DE123456789" },
            new Setting { Id = 10, Key = "Company.BankName", Value = "Musterbank" },
            new Setting { Id = 11, Key = "Company.Iban", Value = "DE89 3704 0044 0532 0130 00" },
            new Setting { Id = 12, Key = "Company.Bic", Value = "MUSTDEXXX" },
            new Setting { Id = 13, Key = "Company.LogoPath", Value = "" }
        );
    }
}