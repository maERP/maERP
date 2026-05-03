using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Dtos.SalesChannelOAuth;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SalesChannelOAuth.Commands.OAuthCallback;

/// <summary>
/// Validates the state token (must exist, not expired, not consumed), exchanges the code for a
/// refresh + access token at the provider's token endpoint via <see cref="IOAuthTokenExchanger"/>,
/// encrypts the tokens onto the SalesChannel via EF (the EncryptedStringConverter handles
/// encryption), and raises a domain notification for audit/orchestration.
/// </summary>
public class OAuthCallbackHandler : IRequestHandler<OAuthCallbackCommand, Result<OAuthCallbackResultDto>>
{
    private readonly IAppLogger<OAuthCallbackHandler> _logger;
    private readonly IOAuthStateRepository _stateRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly IOAuthAppSettingsService _oauthAppSettings;
    private readonly ISettingsService _settings;
    private readonly IOAuthTokenExchanger _tokenExchanger;
    private readonly IMediator _mediator;
    private readonly ITenantContext _tenantContext;

    public OAuthCallbackHandler(
        IAppLogger<OAuthCallbackHandler> logger,
        IOAuthStateRepository stateRepository,
        ISalesChannelRepository salesChannelRepository,
        IOAuthAppSettingsService oauthAppSettings,
        ISettingsService settings,
        IOAuthTokenExchanger tokenExchanger,
        IMediator mediator,
        ITenantContext tenantContext)
    {
        _logger = logger;
        _stateRepository = stateRepository;
        _salesChannelRepository = salesChannelRepository;
        _oauthAppSettings = oauthAppSettings;
        _settings = settings;
        _tokenExchanger = tokenExchanger;
        _mediator = mediator;
        _tenantContext = tenantContext;
    }

    public async Task<Result<OAuthCallbackResultDto>> Handle(
        OAuthCallbackCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.State) || string.IsNullOrEmpty(request.Code))
        {
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.BadRequest,
                "Missing state or code in callback.");
        }

        var state = await _stateRepository.GetByTokenAsync(request.State);
        if (state is null)
        {
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.BadRequest,
                "Unknown state token.");
        }
        if (state.Provider != request.Provider)
        {
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.BadRequest,
                "Provider mismatch between state and callback.");
        }
        if (state.ExpiresAt < DateTime.UtcNow)
        {
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.BadRequest,
                "State token has expired. Restart the OAuth flow.");
        }
        if (state.ConsumedAt is not null)
        {
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.BadRequest,
                "State token has already been consumed.");
        }

        // Mark consumed before code-exchange to enforce single-use even if exchange fails.
        state.ConsumedAt = DateTime.UtcNow;
        await _stateRepository.UpdateAsync(state);

        // The callback endpoint is anonymous (no X-Tenant-Id) — propagate the tenant from the
        // state row so SalesChannelRepository's query filter resolves the right channel.
        _tenantContext.SetCurrentTenantId(state.TenantId);

        var credsResult = await _oauthAppSettings.GetEffectiveCredentialsAsync(
            state.TenantId, state.Provider, cancellationToken);
        if (!credsResult.Succeeded)
        {
            return Result<OAuthCallbackResultDto>.Fail(credsResult.StatusCode,
                string.Join("; ", credsResult.Messages));
        }
        var creds = credsResult.Data;

        var publicBaseUrl = (await _settings.GetSettingValueAsync("OAuth.PublicBaseUrl"))?.TrimEnd('/');
        var browserCallbackUrl = $"{publicBaseUrl}/api/v1/saleschannels/oauth/{request.Provider.ToString().ToLowerInvariant()}/callback";
        // For eBay the redirect_uri at token-exchange is also the RuName (NOT the browser URL).
        var redirectUriForExchange = request.Provider == SalesChannelType.eBay
            ? (creds.RuName ?? string.Empty)
            : browserCallbackUrl;

        OAuthTokenExchangeResult tokenResponse;
        try
        {
            tokenResponse = await _tokenExchanger.ExchangeAuthorizationCodeAsync(
                request.Provider, creds, request.Code, redirectUriForExchange, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("OAuth token exchange failed: {Message}", ex.Message);
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.BadRequest,
                $"Token exchange failed: {ex.Message}");
        }

        var channel = await _salesChannelRepository.GetByIdAsync(state.SalesChannelId);
        if (channel is null)
        {
            return Result<OAuthCallbackResultDto>.Fail(ResultStatusCode.NotFound,
                "SalesChannel disappeared during OAuth flow.");
        }

        channel.AccessToken = tokenResponse.AccessToken;
        // Always store the latest refresh token returned (Amazon doesn't rotate; eBay rarely does).
        if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
        {
            channel.RefreshToken = tokenResponse.RefreshToken;
        }
        if (tokenResponse.ExpiresInSeconds > 0)
        {
            channel.TokenExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresInSeconds);
        }
        await _salesChannelRepository.UpdateAsync(channel);

        await _mediator.Publish(
            new SalesChannelOAuthConnectedNotification(
                channel.Id,
                state.TenantId,
                request.Provider,
                channel.MarketplaceId,
                DateTime.UtcNow),
            cancellationToken);

        _logger.LogInformation(
            "OAuth callback succeeded: tenant {TenantId} channel {ChannelId} provider {Provider}",
            state.TenantId, channel.Id, request.Provider);

        return Result<OAuthCallbackResultDto>.Success(new OAuthCallbackResultDto
        {
            SalesChannelId = channel.Id,
            TenantId = state.TenantId,
            Provider = request.Provider,
            TokenExpiresAt = channel.TokenExpiresAt,
        });
    }
}
