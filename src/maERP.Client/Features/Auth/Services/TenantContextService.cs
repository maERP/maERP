using maERP.Client.Core.Models;
using maERP.Client.Features.Tenants.Services;
using maERP.Domain.Dtos.Tenant;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Service for managing tenant context in the client application.
/// Stores available tenants in memory and manages the current tenant selection.
/// </summary>
public class TenantContextService : ITenantContextService
{
    private readonly ITokenStorageService _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TenantContextService> _logger;
    private List<TenantListDto> _availableTenants = new();
    private TenantListDto? _currentTenant;

    public event EventHandler<TenantListDto?>? CurrentTenantChanged;

    public TenantContextService(
        ITokenStorageService tokenStorage,
        IServiceProvider serviceProvider,
        ILogger<TenantContextService> logger)
    {
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public IReadOnlyList<TenantListDto> AvailableTenants => _availableTenants.AsReadOnly();

    public TenantListDto? CurrentTenant => _currentTenant;

    public Guid? CurrentTenantId => _currentTenant?.Id;

    public async Task SetAvailableTenantsAsync(IEnumerable<TenantListDto> tenants)
    {
        _availableTenants = tenants?.ToList() ?? new List<TenantListDto>();
        _logger.LogInformation("Set {Count} available tenants", _availableTenants.Count);

        // Try to restore the current tenant from storage
        var storedTenantId = await _tokenStorage.GetCurrentTenantIdAsync();
        if (storedTenantId.HasValue)
        {
            var tenant = _availableTenants.FirstOrDefault(t => t.Id == storedTenantId.Value);
            if (tenant != null)
            {
                _currentTenant = tenant;
                _logger.LogInformation("Restored current tenant: {TenantId} ({TenantName})",
                    tenant.Id, tenant.Name);
            }
            else
            {
                _logger.LogWarning("Stored tenant ID {TenantId} not found in available tenants",
                    storedTenantId.Value);
                // Clear invalid tenant ID and select first available
                await SelectFirstAvailableTenantAsync();
            }
        }
        else
        {
            // No stored tenant, select first available
            await SelectFirstAvailableTenantAsync();
        }

        CurrentTenantChanged?.Invoke(this, _currentTenant);
    }

    public async Task SetCurrentTenantAsync(Guid tenantId)
    {
        var tenant = _availableTenants.FirstOrDefault(t => t.Id == tenantId);
        if (tenant == null)
        {
            _logger.LogWarning("Cannot set current tenant: Tenant {TenantId} not found in available tenants",
                tenantId);
            return;
        }

        if (_currentTenant?.Id == tenantId)
        {
            _logger.LogDebug("Tenant {TenantId} is already the current tenant", tenantId);
            return;
        }

        _currentTenant = tenant;
        await _tokenStorage.SetCurrentTenantIdAsync(tenantId);

        _logger.LogInformation("Switched to tenant: {TenantId} ({TenantName})",
            tenant.Id, tenant.Name);

        CurrentTenantChanged?.Invoke(this, _currentTenant);
    }

    public async Task ClearAsync()
    {
        _availableTenants.Clear();
        _currentTenant = null;
        await _tokenStorage.SetCurrentTenantIdAsync(null);

        _logger.LogInformation("Cleared tenant context");

        CurrentTenantChanged?.Invoke(this, null);
    }

    public async Task RefreshTenantsAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Refreshing available tenants from API");

        try
        {
            var tenantService = _serviceProvider.GetRequiredService<ITenantService>();
            var parameters = new QueryParameters { PageNumber = 0, PageSize = 100 };
            var response = await tenantService.GetTenantsAsync(parameters, ct);

            if (response?.Data != null)
            {
                await SetAvailableTenantsAsync(response.Data);
                _logger.LogInformation("Refreshed tenants: {Count} available", response.Data.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh tenants from API");
            throw;
        }
    }

    public async Task<bool> RefreshTenantsAndCheckAvailabilityAsync(CancellationToken ct = default)
    {
        await RefreshTenantsAsync(ct);
        return _availableTenants.Count > 0;
    }

    private async Task SelectFirstAvailableTenantAsync()
    {
        if (_availableTenants.Count > 0)
        {
            var firstTenant = _availableTenants[0];
            _currentTenant = firstTenant;
            await _tokenStorage.SetCurrentTenantIdAsync(firstTenant.Id);
            _logger.LogInformation("Auto-selected first available tenant: {TenantId} ({TenantName})",
                firstTenant.Id, firstTenant.Name);
        }
        else
        {
            _currentTenant = null;
            await _tokenStorage.SetCurrentTenantIdAsync(null);
            _logger.LogInformation("No tenants available, current tenant set to null");
        }
    }
}
