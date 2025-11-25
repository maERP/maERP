namespace maERP.Client.Services.Authentication;

public interface ITokenStorageService
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string token);
    Task ClearTokenAsync();
    Task<string?> GetServerUrlAsync();
    Task SetServerUrlAsync(string serverUrl);
    Task<Guid?> GetCurrentTenantIdAsync();
    Task SetCurrentTenantIdAsync(Guid? tenantId);
}
