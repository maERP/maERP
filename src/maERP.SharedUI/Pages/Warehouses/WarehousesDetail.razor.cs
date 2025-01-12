using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Warehouse;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IWarehouseService _warehouseService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    protected string Title = "Lager";

    protected WarehouseVM warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId != 0)
        {
            Title = "Bearbeiten";
            warehouse = await _warehouseService.GetWarehouseDetails(warehouseId);
        }
        else Title = "nicht gefunden";
    }
}