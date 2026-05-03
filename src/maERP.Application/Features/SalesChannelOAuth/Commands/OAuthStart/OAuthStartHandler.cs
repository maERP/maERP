using System.Security.Cryptography;
using System.Text;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.SalesChannelOAuth;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SalesChannelOAuth.Commands.OAuthStart;

/// <summary>
/// Builds the provider's authorize URL, persists a state-token mapped to the (tenant, channel)
/// tuple, and returns the URL to the Client which then opens it in the system browser.
/// </summary>
public class OAuthStartHandler : IRequestHandler<OAuthStartCommand, Result<OAuthStartResponseDto>>
{
    private const int StateTtlMinutes = 10;

    private readonly IAppLogger<OAuthStartHandler> _logger;
    private readonly ITenantContext _tenantContext;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly IOAuthAppSettingsService _oauthAppSettings;
    private readonly IOAuthStateRepository _stateRepository;
    private readonly ISettingsService _settings;

    public OAuthStartHandler(
        IAppLogger<OAuthStartHandler> logger,
        ITenantContext tenantContext,
        ISalesChannelRepository salesChannelRepository,
        IOAuthAppSettingsService oauthAppSettings,
        IOAuthStateRepository stateRepository,
        ISettingsService settings)
    {
        _logger = logger;
        _tenantContext = tenantContext;
        _salesChannelRepository = salesChannelRepository;
        _oauthAppSettings = oauthAppSettings;
        _stateRepository = stateRepository;
        _settings = settings;
    }

    public async Task<Result<OAuthStartResponseDto>> Handle(
        OAuthStartCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantContext.GetCurrentTenantId();
        if (!tenantId.HasValue)
        {
            return Result<OAuthStartResponseDto>.Fail(ResultStatusCode.BadRequest, "No active tenant.");
        }

        if (request.Provider is not (SalesChannelType.eBay or SalesChannelType.Amazon))
        {
            return Result<OAuthStartResponseDto>.Fail(ResultStatusCode.BadRequest,
                $"OAuth is not supported for provider {request.Provider}.");
        }

        var channel = await _salesChannelRepository.GetByIdAsync(request.SalesChannelId);
        if (channel is null || channel.Type != request.Provider)
        {
            return Result<OAuthStartResponseDto>.Fail(ResultStatusCode.NotFound,
                $"SalesChannel {request.SalesChannelId} not found or its type does not match {request.Provider}.");
        }

        var credsResult = await _oauthAppSettings.GetEffectiveCredentialsAsync(
            tenantId.Value, request.Provider, cancellationToken);
        if (!credsResult.Succeeded)
        {
            return Result<OAuthStartResponseDto>.Fail(credsResult.StatusCode,
                string.Join("; ", credsResult.Messages));
        }
        var creds = credsResult.Data;

        var publicBaseUrl = (await _settings.GetSettingValueAsync("OAuth.PublicBaseUrl"))?.TrimEnd('/');
        if (string.IsNullOrEmpty(publicBaseUrl))
        {
            return Result<OAuthStartResponseDto>.Fail(ResultStatusCode.BadRequest,
                "OAuth.PublicBaseUrl is not configured. Set it in System Settings before starting an OAuth flow.");
        }
        var browserCallbackUrl = $"{publicBaseUrl}/api/v1/saleschannels/oauth/{request.Provider.ToString().ToLowerInvariant()}/callback";

        var stateToken = GenerateRandomBase64Url(32);
        var nonce = GenerateRandomBase64Url(16);

        await _stateRepository.CreateAsync(new OAuthState
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId.Value,
            SalesChannelId = channel.Id,
            Provider = request.Provider,
            StateToken = stateToken,
            Nonce = nonce,
            ExpiresAt = DateTime.UtcNow.AddMinutes(StateTtlMinutes),
            CreatedByUserId = request.UserId,
        });

        var authorizeUrl = request.Provider switch
        {
            SalesChannelType.eBay => BuildEbayAuthorizeUrl(creds, stateToken),
            SalesChannelType.Amazon => BuildAmazonAuthorizeUrl(creds, browserCallbackUrl, stateToken),
            _ => throw new InvalidOperationException($"Unhandled provider {request.Provider}"),
        };

        _logger.LogInformation(
            "OAuth start for tenant {TenantId} channel {ChannelId} provider {Provider} (source={Source})",
            tenantId.Value, channel.Id, request.Provider, creds.Source);

        return Result<OAuthStartResponseDto>.Success(new OAuthStartResponseDto
        {
            AuthorizeUrl = authorizeUrl,
            State = stateToken,
        });
    }

    private static string BuildEbayAuthorizeUrl(
        Application.Contracts.Services.OAuthAppCredentials creds, string state)
    {
        // eBay's authorize endpoint takes the RuName as `redirect_uri` — the actual browser
        // redirect target is configured at developer.ebay.com against the RuName alias.
        var redirectUri = string.IsNullOrEmpty(creds.RuName)
            ? throw new InvalidOperationException("eBay RuName is missing in OAuth settings.")
            : creds.RuName;

        var sb = new StringBuilder(creds.AuthorizationEndpoint);
        sb.Append('?').Append("client_id=").Append(Uri.EscapeDataString(creds.ClientId));
        sb.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri));
        sb.Append("&response_type=code");
        sb.Append("&scope=").Append(Uri.EscapeDataString(creds.Scopes));
        sb.Append("&state=").Append(Uri.EscapeDataString(state));
        return sb.ToString();
    }

    private static string BuildAmazonAuthorizeUrl(
        Application.Contracts.Services.OAuthAppCredentials creds, string browserCallbackUrl, string state)
    {
        // Amazon SP-API: application_id (the SP-API "App ID") + redirect_uri + state.
        // The application_id comes from the OAuth.Amazon.AppId Setting; we read it via ClientId
        // when the tenant override stores the SP-API App ID there. If a separate field is
        // needed, extend the credentials record — for now we route AppId through ClientId for
        // the authorize URL since LWA itself uses ClientId/Secret only on the token endpoint.
        var sb = new StringBuilder(creds.AuthorizationEndpoint);
        sb.Append('?').Append("application_id=").Append(Uri.EscapeDataString(creds.ClientId));
        sb.Append("&redirect_uri=").Append(Uri.EscapeDataString(browserCallbackUrl));
        sb.Append("&state=").Append(Uri.EscapeDataString(state));
        return sb.ToString();
    }

    private static string GenerateRandomBase64Url(int byteCount)
    {
        var bytes = RandomNumberGenerator.GetBytes(byteCount);
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
