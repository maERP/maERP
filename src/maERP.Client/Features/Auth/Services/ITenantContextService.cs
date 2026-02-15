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
    /// Refreshes the available tenants from the API.
    /// Called after creating a new tenant to update the tenant list.
    /// </summary>
    Task RefreshTenantsAsync(CancellationToken ct = default);

    /// <summary>
    /// Refreshes tenants from API and returns whether any tenants are available.
    /// If the current tenant was deleted/deactivated, automatically selects another tenant.
    /// </summary>
    /// <returns>True if at least one tenant is available, false if no tenants remain.</returns>
    Task<bool> RefreshTenantsAndCheckAvailabilityAsync(CancellationToken ct = default);

    /// <summary>
    /// Refreshes the JWT token (to get updated tenant claims) and then refreshes the tenant list.
    /// Call this after creating or modifying tenants to ensure the JWT contains current tenant data.
    /// </summary>
    Task RefreshTokenAndTenantsAsync(CancellationToken ct = default);

    /// <summary>
    /// Event fired when the current tenant changes.
    /// </summary>
    event EventHandler<TenantListDto?>? CurrentTenantChanged;
}
