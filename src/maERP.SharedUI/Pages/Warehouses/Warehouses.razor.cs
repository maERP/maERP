using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.Warehouse;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class Warehouses
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IWarehouseService WarehouseService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVm> _dataGrid = new();

    private async Task<GridData<WarehouseVm>> LoadGridData(GridState<WarehouseVm> state)
    {
        var apiResponse = await WarehouseService.GetWarehouses(state.Page, state.PageSize, _searchString);
        GridData<WarehouseVm> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        await _dataGrid.ReloadServerData();
    }
}