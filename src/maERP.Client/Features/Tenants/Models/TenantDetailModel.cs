using maERP.Client.Core.Constants;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Tenants.Services;
using maERP.Domain.Dtos.Tenant;

namespace maERP.Client.Features.Tenants.Models;

/// <summary>
/// Model for tenant detail page using MVUX pattern.
/// Receives tenant ID from navigation data.
/// </summary>
public partial record TenantDetailModel
{
    private readonly ITenantService _tenantService;
    private readonly INavigator _navigator;
    private readonly Guid _tenantId;

    public TenantDetailModel(
        ITenantService tenantService,
        INavigator navigator,
        TenantDetailData data)
    {
        _tenantService = tenantService;
        _navigator = navigator;
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
            await _navigator.NavigateViewModelAsync<TenantListModel>(this);
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
