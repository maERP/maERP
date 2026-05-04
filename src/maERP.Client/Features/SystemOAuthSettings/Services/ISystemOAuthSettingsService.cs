using maERP.Domain.Dtos.SystemOAuthSettings;

namespace maERP.Client.Features.SystemOAuthSettings.Services;

public interface ISystemOAuthSettingsService
{
    Task<SystemOAuthSettingsDto?> GetAsync(string provider, CancellationToken ct = default);
    Task UpsertAsync(string provider, SystemOAuthSettingsInputDto input, CancellationToken ct = default);
}
