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

        // Ensure a tenant is selected when new data arrives
        if (CurrentTenant == null || !AvailableTenants.Any(t => t.Id == CurrentTenant.Id))
        {
            SetCurrentTenant(AvailableTenants.FirstOrDefault()?.Id);
        }
        else
        {
            // Refresh the tenant header with the existing selection
            _httpService.SetCurrentTenant(CurrentTenant.Id);
        }
    }

    public void SetCurrentTenant(Guid? tenantId)
    {
        Console.WriteLine($"🏢 TenantService.SetCurrentTenant called with: {tenantId}");
        Console.WriteLine($"📊 Available tenants count: {AvailableTenants.Count}");
        
        var tenant = tenantId.HasValue
            ? AvailableTenants.FirstOrDefault(t => t.Id == tenantId.Value)
            : AvailableTenants.FirstOrDefault();

        // Fallback to the first available tenant when no explicit tenant is provided
        if (tenant == null && !tenantId.HasValue && AvailableTenants.Any())
        {
            tenant = AvailableTenants.First();
        }

        Console.WriteLine($"🎯 Selected tenant: {tenant?.Name} (ID: {tenant?.Id})");

        // Always update the HTTP header so API calls receive the current tenant context
        _httpService.SetCurrentTenant(tenant?.Id);

        if (CurrentTenant?.Id != tenant?.Id)
        {
            CurrentTenant = tenant;
            Console.WriteLine($"🔄 Current tenant changed to: {CurrentTenant?.Name}");
            TenantChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Console.WriteLine($"✅ Tenant unchanged: {CurrentTenant?.Name}");
        }
    }

    public async Task SwitchTenantAsync(Guid tenantId)
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
