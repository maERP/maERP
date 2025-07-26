using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
                
                // Order Settings
                new Setting { Key = "Order.NumberFormat", Value = "ORD-{YEAR}-{NUMBER}" },
                new Setting { Key = "Order.DefaultStatus", Value = "New" },
                
                // Notification Settings
                new Setting { Key = "Notification.OrderEmail", Value = "True" },
                new Setting { Key = "Notification.InvoiceEmail", Value = "True" },
                new Setting { Key = "Notification.LowStockAlert", Value = "True" },
                new Setting { Key = "Notification.LowStockThreshold", Value = "5" }
            };
        }
    }
}
