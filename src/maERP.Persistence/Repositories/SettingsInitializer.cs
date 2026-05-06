using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace maERP.Persistence.Repositories
{
    public class SettingsInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SettingsInitializer> _logger;

        public SettingsInitializer(ApplicationDbContext context, ILogger<SettingsInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task EnsureRequiredSettingsExistAsync()
        {
            _logger.LogInformation("Checking for required settings...");

            // Get all required settings
            var requiredSettings = GetRequiredSettings();

            // Get existing settings from database
            var existingSettings = await _context.Setting.ToListAsync();
            var existingKeys = existingSettings.Select(s => s.Key).ToHashSet();

            // Find missing settings
            var missingSettings = requiredSettings
                .Where(s => !existingKeys.Contains(s.Key))
                .ToList();

            if (missingSettings.Count > 0)
            {
                _logger.LogInformation("Adding {Count} missing settings", missingSettings.Count);

                // Don't assign IDs manually - let the database generate them
                foreach (var setting in missingSettings)
                {
                    await _context.Setting.AddAsync(setting);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added missing settings");
            }
            else
            {
                _logger.LogInformation("All required settings are present");
            }
        }

        private static List<Setting> GetRequiredSettings()
        {
            // Define all settings that should exist in the system
            // This reuses settings from SettingsSeeder and adds any additional required settings
            return new List<Setting>
            {
                // Company Information
                new Setting { Key = "Company.Name", Value = "Musterfirma GmbH" },
                new Setting { Key = "Company.Address", Value = "Musterstraße 123" },
                new Setting { Key = "Company.ZipCity", Value = "12345 Musterstadt" },
                new Setting { Key = "Company.Country", Value = "Deutschland" },
                new Setting { Key = "Company.Phone", Value = "+49 123 456789" },
                new Setting { Key = "Company.Email", Value = "info@musterfirma.de" },
                new Setting { Key = "Company.Website", Value = "www.musterfirma.de" },
                new Setting { Key = "Company.TaxId", Value = "123/456/7890" },
                new Setting { Key = "Company.VatId", Value = "DE123456789" },
                new Setting { Key = "Company.BankName", Value = "Musterbank" },
                new Setting { Key = "Company.Iban", Value = "DE89 3704 0044 0532 0130 00" },
                new Setting { Key = "Company.Bic", Value = "MUSTDEXXX" },
                new Setting { Key = "Company.LogoPath", Value = "" },
                
                // System Settings
                new Setting { Key = "System.Theme", Value = "Light" },
                new Setting { Key = "System.DefaultLanguage", Value = "de-DE" },
                new Setting { Key = "System.CurrencyFormat", Value = "€ #,##0.00" },
                new Setting { Key = "System.DateFormat", Value = "dd.MM.yyyy" },
                
                // Invoice Settings
                new Setting { Key = "Invoice.NumberFormat", Value = "INV-{YEAR}-{NUMBER}" },
                new Setting { Key = "Invoice.DefaultPaymentTerms", Value = "14" },
                new Setting { Key = "Invoice.DefaultFooterText", Value = "Vielen Dank für Ihren Einkauf!" },
                
                // Sales Settings
                new Setting { Key = "Sales.NumberFormat", Value = "ORD-{YEAR}-{NUMBER}" },
                new Setting { Key = "Sales.DefaultStatus", Value = "New" },
                
                // Notification Settings
                new Setting { Key = "Notification.SalesEmail", Value = "True" },
                new Setting { Key = "Notification.InvoiceEmail", Value = "True" },
                new Setting { Key = "Notification.LowStockAlert", Value = "True" },
                new Setting { Key = "Notification.LowStockThreshold", Value = "5" },
                
                // JWT Settings
                new Setting { Key = "Jwt.Key", Value = GenerateJwtSecretKey() },
                new Setting { Key = "Jwt.Issuer", Value = "maERP.Server" },
                new Setting { Key = "Jwt.Audience", Value = "maERP.Client" },
                new Setting { Key = "Jwt.DurationInMinutes", Value = "60" },
                new Setting { Key = "Jwt.RefreshTokenExpireDays", Value = "7" },

                // Email Settings - System-wide fallback configuration
                new Setting { Key = "Email.ProviderType", Value = "Smtp" },
                new Setting { Key = "Email.SmtpHost", Value = "localhost" },
                new Setting { Key = "Email.SmtpPort", Value = "1025" },
                new Setting { Key = "Email.SmtpUsername", Value = "" },
                new Setting { Key = "Email.SmtpPassword", Value = "" },
                new Setting { Key = "Email.SmtpEnableSsl", Value = "False" },
                new Setting { Key = "Email.ApiKey", Value = "" },
                new Setting { Key = "Email.FromAddress", Value = "noreply@maerp.local" },
                new Setting { Key = "Email.FromName", Value = "maERP System" },
                new Setting { Key = "Email.ReplyToAddress", Value = "" },
                new Setting { Key = "Email.ReplyToName", Value = "" },

                // Grafana / Loki Settings
                new Setting { Key = "Grafana.Endpoint", Value = "https://grafana.martin-andrich.de" },
                new Setting { Key = "Grafana.LokiEndpoint", Value = "https://grafana.martin-andrich.de/loki" },
                new Setting { Key = "Grafana.MetricsEnabled", Value = "False" },
                new Setting { Key = "Grafana.LogsEnabled", Value = "False" },

                // OAuth — generic
                // Public base URL the third-party redirects back to. Must be HTTPS in production
                // (eBay/Amazon enforce). e.g. "https://erp.acme.com".
                new Setting { Key = "OAuth.PublicBaseUrl", Value = "" },

                // OAuth — eBay (Developer-App fallback when no per-tenant override is set)
                new Setting { Key = "OAuth.Ebay.ClientId", Value = "" },
                new Setting { Key = "OAuth.Ebay.ClientSecret", Value = "", IsEncrypted = true },
                new Setting { Key = "OAuth.Ebay.RuName", Value = "" },
                new Setting { Key = "OAuth.Ebay.AuthorizationEndpoint", Value = "https://auth.ebay.com/oauth2/authorize" },
                new Setting { Key = "OAuth.Ebay.TokenEndpoint", Value = "https://api.ebay.com/identity/v1/oauth2/token" },
                new Setting { Key = "OAuth.Ebay.Scopes", Value = "https://api.ebay.com/oauth/api_scope https://api.ebay.com/oauth/api_scope/sell.inventory https://api.ebay.com/oauth/api_scope/sell.account https://api.ebay.com/oauth/api_scope/sell.fulfillment https://api.ebay.com/oauth/api_scope/sell.marketing https://api.ebay.com/oauth/api_scope/sell.finances" },
                new Setting { Key = "OAuth.Ebay.UseSandbox", Value = "False" },

                // OAuth — Amazon (LWA / SP-API)
                new Setting { Key = "OAuth.Amazon.ClientId", Value = "" },
                new Setting { Key = "OAuth.Amazon.ClientSecret", Value = "", IsEncrypted = true },
                new Setting { Key = "OAuth.Amazon.AppId", Value = "" },
                new Setting { Key = "OAuth.Amazon.RedirectUri", Value = "" },
                new Setting { Key = "OAuth.Amazon.AuthorizationEndpoint", Value = "https://sellercentral.amazon.com/apps/authorize/consent" },
                new Setting { Key = "OAuth.Amazon.TokenEndpoint", Value = "https://api.amazon.com/auth/o2/token" },
                new Setting { Key = "OAuth.Amazon.Scopes", Value = "" },
                new Setting { Key = "OAuth.Amazon.UseSandbox", Value = "False" }
            };
        }

        private static string GenerateJwtSecretKey()
        {
            // Generate a cryptographically secure 256-bit (32 bytes) key
            using var rng = RandomNumberGenerator.Create();
            var keyBytes = new byte[32];
            rng.GetBytes(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }
    }
}
