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

    [ObservableProperty]
    private TenantListDto? selectedTenant;

    [ObservableProperty]
    private bool isVisible;

    public ObservableCollection<TenantListDto> AvailableTenants => _tenantService.AvailableTenants;

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

    [RelayCommand]
    private async Task SelectTenantAsync(TenantListDto? tenant)
    {
        if (tenant != null && tenant.Id != SelectedTenant?.Id)
        {
            await _tenantService.SwitchTenantAsync(tenant.Id);
        }
    }

    public void UpdateVisibility()
    {
        IsVisible = AvailableTenants.Count > 1;
    }
}