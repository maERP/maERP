using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Seeders;

public static class SettingsSeeder
{
    public static void SeedSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Setting>().HasData(
            // JWT Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666614"), Key = "Jwt.Key", Value = "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666615"), Key = "Jwt.Issuer", Value = "maERP.Server" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666616"), Key = "Jwt.Audience", Value = "maERP.Client" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666617"), Key = "Jwt.DurationInMinutes", Value = "60" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666618"), Key = "Jwt.RefreshTokenExpireDays", Value = "7" },

            // Email Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666619"), Key = "Email.ApiKey", Value = "Sendgrid-Key" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666620"), Key = "Email.FromAddress", Value = "no-reply@martin-andrich.de" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666621"), Key = "Email.FromName", Value = "maERP" },

            // Telemetry Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666622"), Key = "Telemetry.Endpoint", Value = "http://localhost:4317" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666623"), Key = "Telemetry.ServiceName", Value = "maERP.Server" }
        );
    }
}