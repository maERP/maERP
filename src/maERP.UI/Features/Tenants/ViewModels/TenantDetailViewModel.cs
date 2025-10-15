using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Tenants.ViewModels;

public partial class TenantDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StatusText))]
    [NotifyPropertyChangedFor(nameof(CanActivate))]
    [NotifyPropertyChangedFor(nameof(CanDeactivate))]
    private TenantDetailDto? tenant;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private Guid tenantId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Tenant != null;
    public string StatusText => Tenant?.IsActive == true ? "Aktiv" : "Inaktiv";
    public bool CanActivate => Tenant?.IsActive != true;
    public bool CanDeactivate => Tenant?.IsActive == true;

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToTenantEdit { get; set; }

    public TenantDetailViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(Guid tenantId)
    {
        TenantId = tenantId;
        await LoadTenantAsync();
    }

    [RelayCommand]
    private async Task LoadTenantAsync()
    {
        if (TenantId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<TenantDetailDto>($"superadmin/tenants/{TenantId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Tenant = null;
                _debugService.LogWarning("GetAsync tenants returned null - not authenticated or missing server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Tenant = result.Data;
                _debugService.LogInfo($"Loaded tenant {TenantId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Tenants {TenantId}";
                Tenant = null;
                _debugService.LogError($"Failed to load tenant {TenantId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
            {
                ErrorMessage = $"Fehler beim Laden des Tenants: {ex.Message}";
                Tenant = null;
                _debugService.LogError(ex, $"Exception loading tenant {TenantId}");
            }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadTenantAsync();
    }

    [RelayCommand]
    private async Task EditTenantAsync()
    {
        if (Tenant == null || NavigateToTenantEdit == null) return;

        await NavigateToTenantEdit(Tenant.Id);
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task ActivateTenantAsync()
    {
        if (!CanActivate || TenantId == Guid.Empty) return;

        await UpdateTenantStatusAsync(true);
    }

    [RelayCommand]
    private async Task DeactivateTenantAsync()
    {
        if (!CanDeactivate || TenantId == Guid.Empty) return;

        await UpdateTenantStatusAsync(false);
    }

    private async Task UpdateTenantStatusAsync(bool isActive)
    {
        if (TenantId == Guid.Empty) return;

        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            var payload = new TenantStatusUpdateDto { IsActive = isActive };
            var result = await _httpService.PutAsync<TenantStatusUpdateDto, TenantDetailDto>($"superadmin/tenants/{TenantId}/status", payload);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("PutAsync tenants status returned null - not authenticated or missing server URL");
                return;
            }

            if (result.Succeeded && result.Data != null)
            {
                Tenant = result.Data;
                _debugService.LogInfo($"Updated tenant {TenantId} status to {(isActive ? "active" : "inactive")}");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Aktualisieren des Tenant-Status";
                _debugService.LogError($"Failed to update tenant {TenantId} status: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Aktualisieren des Tenant-Status: {ex.Message}";
            _debugService.LogError(ex, $"Exception updating tenant {TenantId} status");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
