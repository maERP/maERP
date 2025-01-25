using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IWarehouseService WarehouseService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId > 0)
        {
            await WarehouseService.DeleteWarehouse(warehouseId);
            NavigationManager.NavigateTo("/Warehouses");
        }
    }
}