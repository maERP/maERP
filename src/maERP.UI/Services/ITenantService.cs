using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Tenant;

namespace maERP.UI.Services;

public interface ITenantService
{
    ObservableCollection<TenantListDto> AvailableTenants { get; }
    TenantListDto? CurrentTenant { get; }
    event EventHandler? TenantChanged;

    void SetAvailableTenants(List<TenantListDto> tenants);
    void SetCurrentTenant(Guid? tenantId);
    Task SwitchTenantAsync(Guid tenantId);
}