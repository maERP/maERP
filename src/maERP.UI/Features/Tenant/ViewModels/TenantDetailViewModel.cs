using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Tenant.ViewModels;

public partial class TenantDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
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

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToTenantInput { get; set; }

    // Computed properties for better display
    public string DisplayName => Tenant?.DisplayName ?? string.Empty;

    public bool HasDescription => !string.IsNullOrEmpty(Tenant?.Description);
    public bool HasCompanyName => !string.IsNullOrEmpty(Tenant?.CompanyName);
    public bool HasContactInfo => !string.IsNullOrEmpty(Tenant?.ContactEmail) ||
                                   !string.IsNullOrEmpty(Tenant?.Phone) ||
                                   !string.IsNullOrEmpty(Tenant?.Website);
    public bool HasAddress => !string.IsNullOrEmpty(Tenant?.Street) ||
                              !string.IsNullOrEmpty(Tenant?.City) ||
                              !string.IsNullOrEmpty(Tenant?.Country);
    public bool HasBankingInfo => !string.IsNullOrEmpty(Tenant?.Iban);
    public bool HasAdminInfo => !string.IsNullOrEmpty(Tenant?.AdminEmail) ||
                                 !string.IsNullOrEmpty(Tenant?.Domain) ||
                                 Tenant?.ValidUntil != null;

    public string FullAddress
    {
        get
        {
            if (Tenant == null) return string.Empty;

            var parts = new[]
            {
                Tenant.Street,
                Tenant.Street2,
                $"{Tenant.PostalCode} {Tenant.City}".Trim(),
                Tenant.State,
                Tenant.Country
            }.Where(s => !string.IsNullOrWhiteSpace(s));

            return string.Join(", ", parts);
        }
    }

    public string StatusText => Tenant?.IsActive == true ? "Aktiv" : "Inaktiv";
    public string StatusColor => Tenant?.IsActive == true ? "#10B981" : "#EF4444";
    public string StatusBackgroundColor => Tenant?.IsActive == true ? "#D1FAE5" : "#FEE2E2";

    public bool IsValidUntilExpired => Tenant?.ValidUntil != null && Tenant.ValidUntil < DateTime.Now;
    public string ValidUntilColor => IsValidUntilExpired ? "#EF4444" : "#6B7280";

    public bool CanEdit => Tenant?.CanManageTenant == true;

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
            var result = await _httpService.GetAsync<TenantDetailDto>($"tenants/{TenantId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Tenant = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Tenant = result.Data;
                OnPropertyChanged(nameof(DisplayName));
                OnPropertyChanged(nameof(HasDescription));
                OnPropertyChanged(nameof(HasCompanyName));
                OnPropertyChanged(nameof(HasContactInfo));
                OnPropertyChanged(nameof(HasAddress));
                OnPropertyChanged(nameof(HasBankingInfo));
                OnPropertyChanged(nameof(HasAdminInfo));
                OnPropertyChanged(nameof(FullAddress));
                OnPropertyChanged(nameof(StatusText));
                OnPropertyChanged(nameof(StatusColor));
                OnPropertyChanged(nameof(StatusBackgroundColor));
                OnPropertyChanged(nameof(IsValidUntilExpired));
                OnPropertyChanged(nameof(ValidUntilColor));
                OnPropertyChanged(nameof(CanEdit));
                _debugService.LogInfo($"Loaded tenant {TenantId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Mandanten {TenantId}";
                Tenant = null;
                _debugService.LogError($"Failed to load tenant {TenantId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Mandanten: {ex.Message}";
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
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task EditTenantAsync()
    {
        if (TenantId != Guid.Empty && NavigateToTenantInput != null && CanEdit)
        {
            await NavigateToTenantInput(TenantId);
        }
    }
}
