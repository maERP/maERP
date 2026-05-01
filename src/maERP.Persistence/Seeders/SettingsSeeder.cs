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

            // Email — Provider selection
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666619"), Key = "Email.ProviderType", Value = "Smtp" },

            // Email — SMTP defaults (server-level fallback when no tenant override exists)
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666624"), Key = "Email.SmtpHost", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666625"), Key = "Email.SmtpPort", Value = "587" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666626"), Key = "Email.SmtpUsername", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666627"), Key = "Email.SmtpPassword", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666628"), Key = "Email.SmtpEnableSsl", Value = "true" },

            // Email — Microsoft 365 (Graph API, app-only client credentials)
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666629"), Key = "Email.M365TenantId", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666630"), Key = "Email.M365ClientId", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666631"), Key = "Email.M365ClientSecret", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666632"), Key = "Email.M365SenderAddress", Value = string.Empty },

            // Email — From / Reply-To
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666620"), Key = "Email.FromAddress", Value = "no-reply@martin-andrich.de" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666621"), Key = "Email.FromName", Value = "maERP" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666633"), Key = "Email.ReplyToAddress", Value = string.Empty },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666634"), Key = "Email.ReplyToName", Value = string.Empty },

            // Telemetry Settings
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666622"), Key = "Telemetry.Endpoint", Value = "http://localhost:4317" },
            new Setting { Id = new Guid("66666666-6666-6666-6666-666666666623"), Key = "Telemetry.ServiceName", Value = "maERP.Server" }

            // Grafana / Loki settings are inserted at runtime via SettingsInitializer.
        );
    }
}
