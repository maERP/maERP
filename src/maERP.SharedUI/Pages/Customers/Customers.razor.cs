using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class Customers
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ICustomerService CustomerService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVm> _dataGrid = new();

    private async Task<GridData<CustomerVm>> LoadGridData(GridState<CustomerVm> state)
    {
        var apiResponse = await CustomerService.GetCustomers(state.Page, state.PageSize, _searchString);
        GridData<CustomerVm> data = new()
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