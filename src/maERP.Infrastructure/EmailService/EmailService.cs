using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Models.Email;
using maERP.Domain.Enums;
using maERP.Infrastructure.EmailService.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace maERP.Infrastructure.EmailService;

public class TenantAwareEmailService : IEmailService
{
    private readonly ITenantEmailSettingsRepository _emailSettingsRepository;
    private readonly ITenantContext _tenantContext;
    private readonly IEmailTemplateService _templateService;
    private readonly ISettingsService _settingsService;
    private readonly ILogger<TenantAwareEmailService> _logger;
    private readonly IConfiguration _configuration;
    private readonly Dictionary<EmailProviderType, IEmailProvider> _providers;

    public TenantAwareEmailService(
        ITenantEmailSettingsRepository emailSettingsRepository,
        ITenantContext tenantContext,
        IEmailTemplateService templateService,
        ISettingsService settingsService,
        ILogger<TenantAwareEmailService> logger,
        IConfiguration configuration,
        SmtpEmailProvider smtpProvider,
        SendGridEmailProvider sendGridProvider)
    {
        _emailSettingsRepository = emailSettingsRepository;
        _tenantContext = tenantContext;
        _templateService = templateService;
        _settingsService = settingsService;
        _logger = logger;
        _configuration = configuration;

        // Register available providers
        _providers = new Dictionary<EmailProviderType, IEmailProvider>
        {
            { EmailProviderType.Smtp, smtpProvider },
            { EmailProviderType.SendGrid, sendGridProvider }
        };
    }

    public async Task<bool> SendEmailAsync(EmailMessage email, Guid? tenantId = null)
    {
        try
        {
            var settings = await GetEmailSettingsAsync(tenantId);

            if (settings == null)
            {
                _logger.LogError("No email settings found for tenant {TenantId}", tenantId);
                return false;
            }

            if (!_providers.TryGetValue(settings.ProviderType, out var provider))
            {
                _logger.LogError("No email provider found for type {ProviderType}", settings.ProviderType);
                return false;
            }

            return await provider.SendAsync(email, settings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {To}", email.To);
            return false;
        }
    }

    public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string toName, string resetToken, Guid? tenantId = null)
    {
        try
        {
            // Generate reset URL from configuration
            var resetUrl = _configuration["EmailSettings:PasswordResetUrl"] ?? "https://localhost:5001/reset-password";

            var htmlBody = await _templateService.GeneratePasswordResetEmailAsync(toName, resetToken, resetUrl);

            var emailMessage = new EmailMessage
            {
                To = toEmail,
                ToName = toName,
                Subject = "Passwort zurücksetzen - maERP",
                Body = htmlBody,
                IsHtml = true
            };

            return await SendEmailAsync(emailMessage, tenantId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending password reset email to {Email}", toEmail);
            return false;
        }
    }

    public async Task<bool> SendWelcomeEmailAsync(string toEmail, string toName, Guid? tenantId = null)
    {
        try
        {
            var htmlBody = await _templateService.GenerateWelcomeEmailAsync(toName);

            var emailMessage = new EmailMessage
            {
                To = toEmail,
                ToName = toName,
                Subject = "Willkommen bei maERP",
                Body = htmlBody,
                IsHtml = true
            };

            return await SendEmailAsync(emailMessage, tenantId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending welcome email to {Email}", toEmail);
            return false;
        }
    }

    private async Task<EmailSettings?> GetEmailSettingsAsync(Guid? tenantId)
    {
        // Priority 1: Try to get tenant-specific settings from database
        if (tenantId.HasValue)
        {
            var tenantSettings = await _emailSettingsRepository.GetActiveTenantSettingsAsync(tenantId.Value);
            if (tenantSettings != null)
            {
                _logger.LogDebug("Using tenant-specific email settings for tenant {TenantId}", tenantId);
                return MapToEmailSettings(tenantSettings);
            }
        }

        // Priority 2: Try to get settings for current tenant from context
        var currentTenantId = _tenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            var contextTenantSettings = await _emailSettingsRepository.GetActiveTenantSettingsAsync(currentTenantId.Value);
            if (contextTenantSettings != null)
            {
                _logger.LogDebug("Using context tenant email settings for tenant {TenantId}", currentTenantId);
                return MapToEmailSettings(contextTenantSettings);
            }
        }

        // Priority 3: Fallback to system-wide settings from database (Settings table)
        try
        {
            var systemSettings = await _settingsService.GetEmailSettingsAsync();
            if (systemSettings != null && !string.IsNullOrEmpty(systemSettings.FromAddress))
            {
                _logger.LogDebug("Using system-wide email settings from database");
                return systemSettings;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load system-wide email settings from database");
        }

        // Priority 4: Final fallback to configuration file (appsettings.json)
        _logger.LogDebug("Using default email settings from appsettings.json");
        return GetDefaultEmailSettingsFromConfiguration();
    }

    private EmailSettings MapToEmailSettings(Domain.Entities.TenantEmailSettings tenantSettings)
    {
        return new EmailSettings
        {
            ProviderType = tenantSettings.ProviderType,
            SmtpHost = tenantSettings.SmtpHost,
            SmtpPort = tenantSettings.SmtpPort,
            SmtpUsername = tenantSettings.SmtpUsername,
            SmtpPassword = tenantSettings.SmtpPassword,
            SmtpEnableSsl = tenantSettings.SmtpEnableSsl ?? true,
            ApiKey = tenantSettings.ApiKey,
            FromAddress = tenantSettings.FromAddress,
            FromName = tenantSettings.FromName,
            ReplyToAddress = tenantSettings.ReplyToAddress,
            ReplyToName = tenantSettings.ReplyToName
        };
    }

    private EmailSettings? GetDefaultEmailSettingsFromConfiguration()
    {
        var providerTypeString = _configuration["EmailSettings:ProviderType"];
        if (!Enum.TryParse<EmailProviderType>(providerTypeString, out var providerType))
        {
            providerType = EmailProviderType.Smtp;
        }

        return new EmailSettings
        {
            ProviderType = providerType,
            SmtpHost = _configuration["EmailSettings:SmtpHost"],
            SmtpPort = int.TryParse(_configuration["EmailSettings:SmtpPort"], out var port) ? port : 587,
            SmtpUsername = _configuration["EmailSettings:SmtpUsername"],
            SmtpPassword = _configuration["EmailSettings:SmtpPassword"],
            SmtpEnableSsl = bool.TryParse(_configuration["EmailSettings:SmtpEnableSsl"], out var enableSsl) && enableSsl,
            ApiKey = _configuration["EmailSettings:ApiKey"],
            FromAddress = _configuration["EmailSettings:FromAddress"] ?? "noreply@maerp.com",
            FromName = _configuration["EmailSettings:FromName"] ?? "maERP",
            ReplyToAddress = _configuration["EmailSettings:ReplyToAddress"],
            ReplyToName = _configuration["EmailSettings:ReplyToName"]
        };
    }
}