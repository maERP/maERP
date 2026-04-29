using maERP.Domain.Dtos.ServerInfo;

namespace maERP.Client.Features.Auth.Services;

public interface IServerInfoService
{
    Task<ServerInfoResponseDto?> GetServerInfoAsync(string serverUrl, CancellationToken cancellationToken = default);
}
