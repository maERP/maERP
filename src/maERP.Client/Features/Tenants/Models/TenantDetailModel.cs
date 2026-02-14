using maERP.Client.Core.Constants;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Tenants.Services;
using maERP.Domain.Dtos.Tenant;
using Uno.Extensions.Authentication;

namespace maERP.Client.Features.Tenants.Models;

/// <summary>
/// Model for tenant detail page using MVUX pattern.
/// Receives tenant ID from navigation data.
/// </summary>
public partial record TenantDetailModel
{
    private readonly ITenantService _tenantService;
    private readonly INavigator _navigator;
    private readonly ITenantContextService _tenantContext;
    private readonly IAuthenticationService _authentication;
    private readonly Guid _tenantId;

    public TenantDetailModel(
        ITenantService tenantService,
        INavigator navigator,
        ITenantContextService tenantContext,
        IAuthenticationService authentication,
        TenantDetailData data)
    {
        _tenantService = tenantService;
        _navigator = navigator;
        _tenantContext = tenantContext;
        _authentication = authentication;
        _tenantId = data.tenantId;
    }

    /// <summary>
    /// State for error messages from API operations.
    /// </summary>
    public IState<string> ErrorMessage => State<string>.Value(this, () => string.Empty);

    /// <summary>
    /// Feed that loads the tenant details.
    /// </summary>
    public IFeed<TenantDetailDto> Tenant => Feed.Async(async ct =>
    {
        var tenant = await _tenantService.GetTenantAsync(_tenantId, ct);
        return tenant ?? throw new InvalidOperationException($"Tenant {_tenantId} not found");
    });

    /// <summary>
    /// Navigate to edit tenant page.
    /// </summary>
    public async Task EditTenant()
    {
        await _navigator.NavigateDataAsync(this, new TenantEditData(_tenantId));
    }

    /// <summary>
    /// Clear the error message.
    /// </summary>
    public async Task ClearError(CancellationToken ct)
    {
        await ErrorMessage.Set(string.Empty, ct);
    }

    /// <summary>
    /// Navigate back to tenant list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Navigate to demo data generator page.
    /// </summary>
    public async Task NavigateToDemoDataGenerator(string tenantName)
    {
        await _navigator.NavigateViewModelAsync<DemoDataGeneratorModel>(
            this,
            data: new DemoDataGeneratorData(_tenantId, tenantName));
    }

    /// <summary>
    /// Delete the current tenant.
    /// </summary>
    public async Task DeleteTenant(CancellationToken ct = default)
    {
        try
        {
            await _tenantService.DeleteTenantAsync(_tenantId, ct);

            // Refresh tenant list after deletion
            var hasTenantsRemaining = await _tenantContext.RefreshTenantsAndCheckAvailabilityAsync(ct);

            if (hasTenantsRemaining)
            {
                // Navigate back to tenant list (another tenant was auto-selected)
                await _navigator.NavigateBackAsync(this);
            }
            else
            {
                // No tenants remaining - log out the user
                await _authentication.LogoutAsync(ct);
            }
        }
        catch (ApiException ex)
        {
            await ErrorMessage.Set(ex.CombinedMessage, ct);
        }
        catch (Exception ex)
        {
            await ErrorMessage.Set($"Failed to delete tenant: {ex.Message}", ct);
        }
    }
}

/// <summary>
/// Navigation data for tenant detail page.
/// </summary>
public record TenantDetailData(Guid tenantId);
