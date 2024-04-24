using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Warehouse;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class Warehouses
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IWarehouseService warehouseService { get; set; }

    public List<WarehouseVM>? warehouses { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        warehouses = await warehouseService.GetWarehouses();
    }
}