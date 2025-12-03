using maERP.Domain.Dtos.Tenant;

namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Service for managing tenant context in the client application.
/// Stores available tenants in memory and manages the current tenant selection.
/// </summary>
public interface ITenantContextService
{
    /// <summary>
    /// Gets the list of tenants available to the current user.
    /// </summary>
    IReadOnlyList<TenantListDto> AvailableTenants { get; }

    /// <summary>
    /// Gets the currently selected tenant.
    /// </summary>
    TenantListDto? CurrentTenant { get; }

    /// <summary>
    /// Gets the ID of the currently selected tenant.
    /// </summary>
    Guid? CurrentTenantId { get; }

    /// <summary>
    /// Sets the available tenants for the current user.
    /// Called after successful login with the tenants from LoginResponseDto.
    /// </summary>
    Task SetAvailableTenantsAsync(IEnumerable<TenantListDto> tenants);

    /// <summary>
    /// Sets the current tenant by ID.
    /// Persists the selection to TokenStorageService.
    /// </summary>
    Task SetCurrentTenantAsync(Guid tenantId);

    /// <summary>
    /// Clears all tenant data (called on logout).
    /// </summary>
    Task ClearAsync();

    /// <summary>
    /// Event fired when the current tenant changes.
    /// </summary>
    event EventHandler<TenantListDto?>? CurrentTenantChanged;
}
