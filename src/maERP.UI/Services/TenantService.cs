using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Tenant;

namespace maERP.UI.Services;

public class TenantService : ITenantService
{
    public ObservableCollection<TenantListDto> AvailableTenants { get; } = new();
    public TenantListDto? CurrentTenant { get; private set; }
    public event EventHandler? TenantChanged;

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

        if (CurrentTenant?.Id != tenant?.Id)
        {
            CurrentTenant = tenant;
            TenantChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task SwitchTenantAsync(int tenantId)
    {
        var tenant = AvailableTenants.FirstOrDefault(t => t.Id == tenantId);
        if (tenant != null && CurrentTenant?.Id != tenantId)
        {
            CurrentTenant = tenant;
            TenantChanged?.Invoke(this, EventArgs.Empty);
        }

        // Hier könnte später ein API-Call eingefügt werden um den Tenant zu wechseln
        await Task.CompletedTask;
    }
}