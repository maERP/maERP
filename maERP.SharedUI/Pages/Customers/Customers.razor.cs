using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class Customers
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ICustomerService _customerService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVM> _dataGrid = new();

    private async Task<GridData<CustomerVM>> LoadGridData(GridState<CustomerVM> state)
    {
        var apiResponse = await _customerService.GetCustomers(state.Page, state.PageSize, _searchString);
        GridData<CustomerVM> data = new()
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