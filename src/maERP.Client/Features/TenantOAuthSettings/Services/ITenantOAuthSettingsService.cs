using maERP.Domain.Dtos.TenantOAuthAppSettings;

namespace maERP.Client.Features.TenantOAuthSettings.Services;

public interface ITenantOAuthSettingsService
{
    Task<TenantOAuthAppSettingsDetailDto?> GetAsync(string provider, CancellationToken ct = default);
    Task UpsertAsync(string provider, TenantOAuthAppSettingsInputDto input, CancellationToken ct = default);
    Task DeleteAsync(string provider, CancellationToken ct = default);
}
