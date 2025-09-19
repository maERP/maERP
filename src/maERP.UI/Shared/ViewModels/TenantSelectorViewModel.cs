using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;

namespace maERP.UI.Shared.ViewModels;

public partial class TenantSelectorViewModel : ViewModelBase
{
    private readonly ITenantService _tenantService;

    private TenantListDto? _selectedTenant;

    public TenantListDto? SelectedTenant
    {
        get => _selectedTenant;
        set
        {
            if (SetProperty(ref _selectedTenant, value) && value != null && value.Id != _tenantService.CurrentTenant?.Id)
            {
                // Switch tenant and navigate to dashboard
                _ = SwitchTenantAndNavigateAsync(value);
            }
        }
    }

    private async Task SwitchTenantAndNavigateAsync(TenantListDto tenant)
    {
        try
        {
            await _tenantService.SwitchTenantAsync(tenant.Id);

            if (NavigateToMenuItem != null)
            {
                await NavigateToMenuItem("Dashboard");
            }
        }
        catch (Exception ex)
        {
            // Log error or show to user
            System.Diagnostics.Debug.WriteLine($"Error switching tenant: {ex.Message}");
        }
    }

    [ObservableProperty]
    private bool isVisible;

    public ObservableCollection<TenantListDto> AvailableTenants => _tenantService.AvailableTenants;

    public Func<string, Task>? NavigateToMenuItem { get; set; }

    public TenantSelectorViewModel(ITenantService tenantService)
    {
        _tenantService = tenantService;
        _tenantService.TenantChanged += OnTenantChanged;

        // Initial setup
        SelectedTenant = _tenantService.CurrentTenant;
        IsVisible = AvailableTenants.Count > 1;
    }

    private void OnTenantChanged(object? sender, EventArgs e)
    {
        SelectedTenant = _tenantService.CurrentTenant;
        IsVisible = AvailableTenants.Count > 1;
    }


    public void UpdateVisibility()
    {
        IsVisible = AvailableTenants.Count > 1;
    }
}