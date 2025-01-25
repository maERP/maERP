using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Orders;

public partial class Orders
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IOrderService OrderService { get; set; }

    private string _searchString = string.Empty;

    private readonly MudDataGrid<OrderListVm> _dataGrid = new();

    private async Task<GridData<OrderListVm>> LoadGridData(GridState<OrderListVm> state)
    {
        var apiResponse = await OrderService.GetOrders(state.Page, state.PageSize, _searchString);
        GridData<OrderListVm> data = new()
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