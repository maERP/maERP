using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Models.Email;
using maERP.Domain.Enums;
using maERP.Infrastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace maERP.Server.Tests.Features.TenantEmailSettings;

public class TenantAwareEmailServiceFallbackTest
{
    [Fact]
    public async Task TenantOverride_FillsGapsFromServerDefaults()
    {
        var serverSettings = new EmailSettings
        {
            ProviderType = EmailProviderType.Smtp,
            SmtpHost = "smtp.server",
            SmtpPort = 587,
            SmtpUsername = "server-user",
            SmtpPassword = "server-secret",
            SmtpEnableSsl = true,
            FromAddress = "server@example.com",
            FromName = "Server",
            ReplyToAddress = "noreply@example.com"
        };

        var tenantOverride = new Domain.Entities.TenantEmailSettings
        {
            TenantId = Guid.NewGuid(),
            ProviderType = EmailProviderType.Smtp,
            IsActive = true,
            FromName = "Tenant Override"
        };

        var smtp = new CapturingProvider(EmailProviderType.Smtp);
        var service = BuildService(serverSettings, tenantOverride, smtp);

        var sent = await service.SendEmailAsync(new EmailMessage
        {
            To = "to@example.com",
            ToName = "To",
            Subject = "Subject",
            Body = "Body"
        }, tenantOverride.TenantId);

        Assert.True(sent);
        Assert.NotNull(smtp.LastSettings);
        Assert.Equal("smtp.server", smtp.LastSettings!.SmtpHost);
        Assert.Equal(587, smtp.LastSettings.SmtpPort);
        Assert.Equal("server-user", smtp.LastSettings.SmtpUsername);
        Assert.Equal("server-secret", smtp.LastSettings.SmtpPassword);
        Assert.Equal("server@example.com", smtp.LastSettings.FromAddress);
        Assert.Equal("Tenant Override", smtp.LastSettings.FromName);
        Assert.Equal("noreply@example.com", smtp.LastSettings.ReplyToAddress);
    }

    [Fact]
    public async Task TenantOverride_WinsWhenSet_AndDispatchesToCorrectProvider()
    {
        var serverSettings = new EmailSettings
        {
            ProviderType = EmailProviderType.Smtp,
            SmtpHost = "smtp.server",
            FromAddress = "server@example.com",
            FromName = "Server"
        };

        var tenantOverride = new Domain.Entities.TenantEmailSettings
        {
            TenantId = Guid.NewGuid(),
            ProviderType = EmailProviderType.Microsoft365,
            IsActive = true,
            M365TenantId = "tid",
            M365ClientId = "cid",
            M365ClientSecret = "secret",
            M365SenderAddress = "tenant@example.com",
            FromAddress = "tenant@example.com",
            FromName = "Tenant"
        };

        var smtp = new CapturingProvider(EmailProviderType.Smtp);
        var m365 = new CapturingProvider(EmailProviderType.Microsoft365);
        var service = BuildService(serverSettings, tenantOverride, smtp, m365);

        var sent = await service.SendEmailAsync(new EmailMessage
        {
            To = "to@example.com",
            Subject = "S",
            Body = "B"
        }, tenantOverride.TenantId);

        Assert.True(sent);
        Assert.Null(smtp.LastSettings);
        Assert.NotNull(m365.LastSettings);
        Assert.Equal("tenant@example.com", m365.LastSettings!.FromAddress);
        Assert.Equal("tid", m365.LastSettings.M365TenantId);
    }

    [Fact]
    public async Task NoTenantOverride_UsesServerSettingsAsIs()
    {
        var serverSettings = new EmailSettings
        {
            ProviderType = EmailProviderType.Smtp,
            SmtpHost = "smtp.server",
            FromAddress = "server@example.com",
            FromName = "Server"
        };

        var smtp = new CapturingProvider(EmailProviderType.Smtp);
        var service = BuildService(serverSettings, tenantOverride: null, smtp);

        var sent = await service.SendEmailAsync(new EmailMessage
        {
            To = "to@example.com",
            Subject = "S",
            Body = "B"
        }, Guid.NewGuid());

        Assert.True(sent);
        Assert.Equal("smtp.server", smtp.LastSettings!.SmtpHost);
        Assert.Equal("server@example.com", smtp.LastSettings.FromAddress);
    }

    private static TenantAwareEmailService BuildService(
        EmailSettings serverSettings,
        Domain.Entities.TenantEmailSettings? tenantOverride,
        params IEmailProvider[] providers)
    {
        return new TenantAwareEmailService(
            new StubTenantEmailSettingsRepository(tenantOverride),
            new StubTenantContext(),
            new StubTemplateService(),
            new StubSettingsService(serverSettings),
            NullLogger<TenantAwareEmailService>.Instance,
            new ConfigurationBuilder().Build(),
            providers);
    }

    private sealed class CapturingProvider : IEmailProvider
    {
        public CapturingProvider(EmailProviderType type) => ProviderType = type;
        public EmailProviderType ProviderType { get; }
        public EmailSettings? LastSettings { get; private set; }
        public Task<bool> SendAsync(EmailMessage email, EmailSettings settings)
        {
            LastSettings = settings;
            return Task.FromResult(true);
        }
    }

    private sealed class StubSettingsService : ISettingsService
    {
        private readonly EmailSettings _email;
        public StubSettingsService(EmailSettings email) => _email = email;
        public Task<maERP.Application.Models.Identity.JwtSettings> GetJwtSettingsAsync() => Task.FromResult(new maERP.Application.Models.Identity.JwtSettings());
        public Task<EmailSettings> GetEmailSettingsAsync() => Task.FromResult(_email);
        public Task<maERP.Application.Models.Telemetry.TelemetrySettings> GetTelemetrySettingsAsync() => Task.FromResult(new maERP.Application.Models.Telemetry.TelemetrySettings());
        public Task<maERP.Application.Models.Grafana.GrafanaSettings> GetGrafanaSettingsAsync() => Task.FromResult(new maERP.Application.Models.Grafana.GrafanaSettings());
        public Task<string> GetSettingValueAsync(string key) => Task.FromResult(string.Empty);
        public Task SetSettingValueAsync(string key, string value) => Task.CompletedTask;
        public Task<string> GetEncryptedSettingValueAsync(string key) => Task.FromResult(string.Empty);
        public Task SetEncryptedSettingValueAsync(string key, string value) => Task.CompletedTask;
    }

    private sealed class StubTenantEmailSettingsRepository : ITenantEmailSettingsRepository
    {
        private readonly Domain.Entities.TenantEmailSettings? _override;
        public StubTenantEmailSettingsRepository(Domain.Entities.TenantEmailSettings? @override) => _override = @override;
        public IQueryable<Domain.Entities.TenantEmailSettings> Entities => throw new NotImplementedException();
        public Task<Domain.Entities.TenantEmailSettings?> GetByTenantIdAsync(Guid tenantId)
            => Task.FromResult(_override != null && _override.TenantId == tenantId ? _override : null);
        public Task<Domain.Entities.TenantEmailSettings?> GetActiveTenantSettingsAsync(Guid tenantId)
            => Task.FromResult(_override != null && _override.TenantId == tenantId && _override.IsActive ? _override : null);
        public Task<Guid> CreateAsync(Domain.Entities.TenantEmailSettings entity) => throw new NotImplementedException();
        public Task<ICollection<Domain.Entities.TenantEmailSettings>> GetAllAsync() => throw new NotImplementedException();
        public Task<Domain.Entities.TenantEmailSettings?> GetByIdAsync(Guid id, bool asNoTracking = false) => throw new NotImplementedException();
        public Task UpdateAsync(Domain.Entities.TenantEmailSettings entity) => throw new NotImplementedException();
        public Task DeleteAsync(Domain.Entities.TenantEmailSettings entity) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(Guid id) => throw new NotImplementedException();
        public Task<bool> ExistsGloballyAsync(Guid id) => throw new NotImplementedException();
        public Task<bool> IsUniqueAsync(Domain.Entities.TenantEmailSettings entity, Guid? id = null) => throw new NotImplementedException();
        public void Attach(Domain.Entities.TenantEmailSettings entity) => throw new NotImplementedException();
        public void AttachRange(IEnumerable<Domain.Entities.TenantEmailSettings> entities) => throw new NotImplementedException();
        public IQueryable<TCt> GetContext<TCt>() where TCt : class => throw new NotImplementedException();
        public Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public void Add(Domain.Entities.TenantEmailSettings entity) => throw new NotImplementedException();
    }

    private sealed class StubTenantContext : ITenantContext
    {
        public Guid? GetCurrentTenantId() => null;
        public void SetCurrentTenantId(Guid? tenantId) { }
        public bool HasTenant() => false;
        public IReadOnlyCollection<Guid> GetAssignedTenantIds() => Array.Empty<Guid>();
        public void SetAssignedTenantIds(IEnumerable<Guid> tenantIds) { }
        public bool IsAssignedToTenant(Guid tenantId) => false;
    }

    private sealed class StubTemplateService : IEmailTemplateService
    {
        public Task<string> GeneratePasswordResetEmailAsync(string toName, string resetToken, string resetUrl) => Task.FromResult(string.Empty);
        public Task<string> GenerateWelcomeEmailAsync(string toName) => Task.FromResult(string.Empty);
        public Task<string> GenerateEmailConfirmationAsync(string toName, string confirmationToken, string confirmationUrl) => Task.FromResult(string.Empty);
    }
}
