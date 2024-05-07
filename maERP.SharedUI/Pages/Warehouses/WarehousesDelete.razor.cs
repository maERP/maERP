using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IWarehouseService _warehouseService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId > 0)
        {
            await _warehouseService.DeleteWarehouse(warehouseId);
            _navigationManager.NavigateTo("/Warehouses");
        }
    }
}