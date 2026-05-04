using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Mediator;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SystemOAuthSettings.Commands.SystemOAuthSettingsUpsert;

public class SystemOAuthSettingsUpsertHandler
    : IRequestHandler<SystemOAuthSettingsUpsertCommand, Result<int>>
{
    private readonly IAppLogger<SystemOAuthSettingsUpsertHandler> _logger;
    private readonly ISettingsService _settings;

    public SystemOAuthSettingsUpsertHandler(
        IAppLogger<SystemOAuthSettingsUpsertHandler> logger,
        ISettingsService settings)
    {
        _logger = logger;
        _settings = settings;
    }

    public async Task<Result<int>> Handle(
        SystemOAuthSettingsUpsertCommand request, CancellationToken cancellationToken)
    {
        if (request.Provider is not (SalesChannelType.eBay or SalesChannelType.Amazon))
        {
            return Result<int>.Fail(ResultStatusCode.BadRequest,
                $"OAuth provider {request.Provider} is not supported.");
        }

        var prefix = request.Provider.ToString();

        await _settings.SetSettingValueAsync($"OAuth.{prefix}.ClientId", request.ClientId ?? string.Empty);
        if (request.ClientSecret is not null)
        {
            await _settings.SetEncryptedSettingValueAsync($"OAuth.{prefix}.ClientSecret", request.ClientSecret);
        }
        await _settings.SetSettingValueAsync($"OAuth.{prefix}.RuName", request.RuName ?? string.Empty);
        await _settings.SetSettingValueAsync($"OAuth.{prefix}.RedirectUri", request.RedirectUri ?? string.Empty);
        await _settings.SetSettingValueAsync($"OAuth.{prefix}.AuthorizationEndpoint", request.AuthorizationEndpoint ?? string.Empty);
        await _settings.SetSettingValueAsync($"OAuth.{prefix}.TokenEndpoint", request.TokenEndpoint ?? string.Empty);
        await _settings.SetSettingValueAsync($"OAuth.{prefix}.Scopes", request.Scopes ?? string.Empty);
        await _settings.SetSettingValueAsync($"OAuth.{prefix}.UseSandbox", request.UseSandbox.ToString());

        if (request.PublicBaseUrl is not null)
        {
            await _settings.SetSettingValueAsync("OAuth.PublicBaseUrl", request.PublicBaseUrl);
        }

        _logger.LogInformation("Upserted system OAuth settings for provider {Provider}", request.Provider);
        return new Result<int> { Succeeded = true, Data = 1, StatusCode = ResultStatusCode.NoContent };
    }
}
