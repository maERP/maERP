using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Seeders;

public static class SettingsSeeder
{
    public static void SeedSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Setting>().HasData(
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666601"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Name", Value = "Musterfirma GmbH" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666602"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Address", Value = "Musterstraße 123" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666603"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.ZipCity", Value = "12345 Musterstadt" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666604"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Country", Value = "Deutschland" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666605"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Phone", Value = "+49 123 456789" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666606"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Email", Value = "info@musterfirma.de" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666607"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Website", Value = "www.musterfirma.de" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666608"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.TaxId", Value = "123/456/7890" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666609"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.VatId", Value = "DE123456789" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666610"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.BankName", Value = "Musterbank" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666611"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Iban", Value = "DE89 3704 0044 0532 0130 00" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666612"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.Bic", Value = "MUSTDEXXX" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666613"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Company.LogoPath", Value = "" },

            // JWT Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666614"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Jwt.Key", Value = "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666615"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Jwt.Issuer", Value = "maERP.Server" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666616"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Jwt.Audience", Value = "maERP.Client" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666617"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Jwt.DurationInMinutes", Value = "60" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666618"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Jwt.RefreshTokenExpireDays", Value = "7" },

            // Email Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666619"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Email.ApiKey", Value = "Sendgrid-Key" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666620"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Email.FromAddress", Value = "no-reply@martin-andrich.de" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666621"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Email.FromName", Value = "maERP" },

            // Telemetry Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666622"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Telemetry.Endpoint", Value = "http://localhost:4317" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666623"), TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Key = "Telemetry.ServiceName", Value = "maERP.Server" }
        );
    }
}