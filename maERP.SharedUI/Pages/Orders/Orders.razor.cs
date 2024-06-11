using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Orders;

public partial class Orders
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IOrderService _orderService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVM> _dataGrid = new();

    private async Task<GridData<OrderListVM>> LoadGridData(GridState<OrderListVM> state)
    {
        var apiResponse = await _orderService.GetOrders(state.Page, state.PageSize, _searchString);
        GridData<OrderListVM> data = new()
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