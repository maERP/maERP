using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class Warehouses
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IWarehouseService _warehouseService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVM> _dataGrid = new();

    private async Task<GridData<WarehouseVM>> LoadGridData(GridState<WarehouseVM> state)
    {
        var apiResponse = await _warehouseService.GetWarehouses(state.Page, state.PageSize, _searchString);
        GridData<WarehouseVM> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        if (_dataGrid is not null)
        {
            await _dataGrid!.ReloadServerData();
        }
    }
}