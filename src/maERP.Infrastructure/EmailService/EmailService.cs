using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Models.Email;
using maERP.Domain.Enums;
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
        IEnumerable<IEmailProvider> providers)
    {
        _emailSettingsRepository = emailSettingsRepository;
        _tenantContext = tenantContext;
        _templateService = templateService;
        _settingsService = settingsService;
        _logger = logger;
        _configuration = configuration;

        _providers = providers.ToDictionary(p => p.ProviderType);
    }

    public async Task<bool> SendEmailAsync(EmailMessage email, Guid? tenantId = null)
    {
        try
        {
            var settings = await GetEmailSettingsAsync(tenantId);

            if (settings == null || string.IsNullOrWhiteSpace(settings.FromAddress))
            {
                _logger.LogError("No usable email settings found for tenant {TenantId}", tenantId);
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
        // 1. Server-level defaults — Setting table first, otherwise appsettings.json.
        var serverSettings = await LoadServerSettingsAsync();

        // 2. Tenant override (if any) merged on top of the server defaults.
        var resolvedTenantId = tenantId ?? _tenantContext.GetCurrentTenantId();
        if (resolvedTenantId.HasValue)
        {
            var tenantSettings = await _emailSettingsRepository.GetActiveTenantSettingsAsync(resolvedTenantId.Value);
            if (tenantSettings != null)
            {
                _logger.LogDebug("Merging tenant email settings for tenant {TenantId} onto server defaults", resolvedTenantId);
                return MergeWithTenant(serverSettings, tenantSettings);
            }
        }

        return serverSettings;
    }

    private async Task<EmailSettings> LoadServerSettingsAsync()
    {
        try
        {
            var systemSettings = await _settingsService.GetEmailSettingsAsync();
            if (systemSettings != null && !string.IsNullOrWhiteSpace(systemSettings.FromAddress))
            {
                _logger.LogDebug("Using system-wide email settings from Setting table");
                return systemSettings;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load system-wide email settings from database");
        }

        _logger.LogDebug("Falling back to email settings from appsettings.json");
        return GetDefaultEmailSettingsFromConfiguration();
    }

    private static EmailSettings MergeWithTenant(EmailSettings server, Domain.Entities.TenantEmailSettings tenant)
    {
        return new EmailSettings
        {
            ProviderType = tenant.ProviderType,
            SmtpHost = Coalesce(tenant.SmtpHost, server.SmtpHost),
            SmtpPort = tenant.SmtpPort ?? server.SmtpPort,
            SmtpUsername = Coalesce(tenant.SmtpUsername, server.SmtpUsername),
            SmtpPassword = Coalesce(tenant.SmtpPassword, server.SmtpPassword),
            SmtpEnableSsl = tenant.SmtpEnableSsl ?? server.SmtpEnableSsl,
            M365TenantId = Coalesce(tenant.M365TenantId, server.M365TenantId),
            M365ClientId = Coalesce(tenant.M365ClientId, server.M365ClientId),
            M365ClientSecret = Coalesce(tenant.M365ClientSecret, server.M365ClientSecret),
            M365SenderAddress = Coalesce(tenant.M365SenderAddress, server.M365SenderAddress),
            FromAddress = Coalesce(tenant.FromAddress, server.FromAddress) ?? string.Empty,
            FromName = Coalesce(tenant.FromName, server.FromName) ?? string.Empty,
            ReplyToAddress = Coalesce(tenant.ReplyToAddress, server.ReplyToAddress),
            ReplyToName = Coalesce(tenant.ReplyToName, server.ReplyToName)
        };
    }

    private static string? Coalesce(string? primary, string? fallback)
        => string.IsNullOrWhiteSpace(primary) ? fallback : primary;

    private EmailSettings GetDefaultEmailSettingsFromConfiguration()
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
            SmtpEnableSsl = !bool.TryParse(_configuration["EmailSettings:SmtpEnableSsl"], out var enableSsl) || enableSsl,
            M365TenantId = _configuration["EmailSettings:M365TenantId"],
            M365ClientId = _configuration["EmailSettings:M365ClientId"],
            M365ClientSecret = _configuration["EmailSettings:M365ClientSecret"],
            M365SenderAddress = _configuration["EmailSettings:M365SenderAddress"],
            FromAddress = _configuration["EmailSettings:FromAddress"] ?? "noreply@maerp.com",
            FromName = _configuration["EmailSettings:FromName"] ?? "maERP",
            ReplyToAddress = _configuration["EmailSettings:ReplyToAddress"],
            ReplyToName = _configuration["EmailSettings:ReplyToName"]
        };
    }
}
