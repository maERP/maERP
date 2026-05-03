using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SalesChannelOAuth.Commands.OAuthDisconnect;

/// <summary>
/// Clears the SalesChannel's stored refresh + access tokens. The OAuth Developer-App credentials
/// (in <c>TenantOAuthAppSettings</c> / system <c>Setting</c>) are NOT touched — disconnect is
/// per-channel; the next "Connect" can re-link without re-entering app credentials.
/// </summary>
public class OAuthDisconnectHandler : IRequestHandler<OAuthDisconnectCommand, Result<int>>
{
    private readonly IAppLogger<OAuthDisconnectHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public OAuthDisconnectHandler(
        IAppLogger<OAuthDisconnectHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<Result<int>> Handle(OAuthDisconnectCommand request, CancellationToken cancellationToken)
    {
        var channel = await _salesChannelRepository.GetByIdAsync(request.SalesChannelId);
        if (channel is null)
        {
            return Result<int>.Fail(ResultStatusCode.NotFound, "SalesChannel not found.");
        }

        channel.AccessToken = null;
        channel.RefreshToken = null;
        channel.TokenExpiresAt = null;

        await _salesChannelRepository.UpdateAsync(channel);

        _logger.LogInformation("OAuth disconnect for channel {ChannelId}", channel.Id);

        return new Result<int> { Succeeded = true, Data = 1, StatusCode = ResultStatusCode.NoContent };
    }
}
