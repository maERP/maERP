using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Warehouse;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IWarehouseService WarehouseService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    protected string Title = "Lager";

    protected WarehouseVm Warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId != 0)
        {
            Title = "Bearbeiten";
            Warehouse = await WarehouseService.GetWarehouseDetails(warehouseId);
        }
        else Title = "nicht gefunden";
    }
}