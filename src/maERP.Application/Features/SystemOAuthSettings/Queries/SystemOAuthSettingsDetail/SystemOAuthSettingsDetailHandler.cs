using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.SystemOAuthSettings;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SystemOAuthSettings.Queries.SystemOAuthSettingsDetail;

/// <summary>
/// Reads the bundle of <c>OAuth.{Provider}.*</c> Setting rows for a single provider.
/// <c>ClientSecret</c> is decrypted by the SettingsService (per-row IsEncrypted flag) but the
/// DTO only signals its presence — never the value itself.
/// </summary>
public class SystemOAuthSettingsDetailHandler
    : IRequestHandler<SystemOAuthSettingsDetailQuery, Result<SystemOAuthSettingsDto>>
{
    private readonly IAppLogger<SystemOAuthSettingsDetailHandler> _logger;
    private readonly ISettingsService _settings;

    public SystemOAuthSettingsDetailHandler(
        IAppLogger<SystemOAuthSettingsDetailHandler> logger,
        ISettingsService settings)
    {
        _logger = logger;
        _settings = settings;
    }

    public async Task<Result<SystemOAuthSettingsDto>> Handle(
        SystemOAuthSettingsDetailQuery request, CancellationToken cancellationToken)
    {
        if (request.Provider is not (SalesChannelType.eBay or SalesChannelType.Amazon))
        {
            return Result<SystemOAuthSettingsDto>.Fail(ResultStatusCode.BadRequest,
                $"OAuth provider {request.Provider} is not supported.");
        }

        var prefix = request.Provider.ToString();
        var clientSecretCipher = await _settings.GetSettingValueAsync($"OAuth.{prefix}.ClientSecret");

        return Result<SystemOAuthSettingsDto>.Success(new SystemOAuthSettingsDto
        {
            Provider = request.Provider,
            ClientId = await _settings.GetSettingValueAsync($"OAuth.{prefix}.ClientId"),
            HasClientSecret = !string.IsNullOrEmpty(clientSecretCipher),
            RuName = await _settings.GetSettingValueAsync($"OAuth.{prefix}.RuName"),
            RedirectUri = await _settings.GetSettingValueAsync($"OAuth.{prefix}.RedirectUri"),
            AuthorizationEndpoint = await _settings.GetSettingValueAsync($"OAuth.{prefix}.AuthorizationEndpoint"),
            TokenEndpoint = await _settings.GetSettingValueAsync($"OAuth.{prefix}.TokenEndpoint"),
            Scopes = await _settings.GetSettingValueAsync($"OAuth.{prefix}.Scopes"),
            UseSandbox = bool.TryParse(await _settings.GetSettingValueAsync($"OAuth.{prefix}.UseSandbox"), out var s) && s,
            PublicBaseUrl = await _settings.GetSettingValueAsync("OAuth.PublicBaseUrl"),
        });
    }
}
