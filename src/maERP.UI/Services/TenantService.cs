using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Tenant;

namespace maERP.UI.Services;

public class TenantService : ITenantService
{
    private readonly IHttpService _httpService;
    public ObservableCollection<TenantListDto> AvailableTenants { get; } = new();
    public TenantListDto? CurrentTenant { get; private set; }
    public event EventHandler? TenantChanged;

    public TenantService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public void SetAvailableTenants(List<TenantListDto> tenants)
    {
        AvailableTenants.Clear();
        foreach (var tenant in tenants)
        {
            AvailableTenants.Add(tenant);
        }
    }

    public void SetCurrentTenant(int? tenantId)
    {
        var tenant = tenantId.HasValue
            ? AvailableTenants.FirstOrDefault(t => t.Id == tenantId.Value)
            : AvailableTenants.FirstOrDefault();

        // If no specific tenant was requested and no tenant was found, 
        // and we have available tenants, select the first one
        if (tenant == null && !tenantId.HasValue && AvailableTenants.Any())
        {
            tenant = AvailableTenants.First();
        }

        if (CurrentTenant?.Id != tenant?.Id)
        {
            CurrentTenant = tenant;

            // Inform HttpService about the tenant change
            _httpService.SetCurrentTenant(tenant?.Id);

            TenantChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task SwitchTenantAsync(int tenantId)
    {
        var tenant = AvailableTenants.FirstOrDefault(t => t.Id == tenantId);
        if (tenant != null && CurrentTenant?.Id != tenantId)
        {
            CurrentTenant = tenant;

            // Inform HttpService about the tenant switch
            _httpService.SetCurrentTenant(tenantId);

            TenantChanged?.Invoke(this, EventArgs.Empty);
        }

        await Task.CompletedTask;
    }
}