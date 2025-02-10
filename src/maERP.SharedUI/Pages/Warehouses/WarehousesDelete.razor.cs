using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required HttpService HttpService { get; set; }
    
    [Parameter]
    public int warehouseId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId > 0)
        {
            await HttpService.DeleteAsync("/api/v1/Warehouses/" + warehouseId);
            NavigationManager.NavigateTo("/Warehouses");
        }
    }
}