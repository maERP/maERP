using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Parameter]
    public int CustomerId { get; set; }

    private string _title = "Kundendetails";

    private CustomerDetailDto _customerDetail = new();
    private MudDataGrid<OrderListDto> _customerOrderListGrid = new();
    private string _customerOrderSearchString = "";

    protected override async Task OnParametersSetAsync()
    {
        if (CustomerId != 0)
        {
            var result = await HttpService.GetAsync<Result<CustomerDetailDto>>($"/api/v1/Customers/{CustomerId}");
            
            if (result != null && result.Succeeded)
            {
                _title = $"Kunde - {_customerDetail.Firstname} {_customerDetail.Lastname}";
                _customerDetail = result.Data;
            }
        }
    }
    
    private async Task<GridData<OrderListDto>> LoadGridData(GridState<OrderListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateCreated Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<OrderListDto>>(
                              $"/api/v1/Orders/customer/{CustomerId}?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_customerOrderSearchString}&orderBy={orderBy}")
                          ?? new PaginatedResult<OrderListDto>(new List<OrderListDto>());

        GridData<OrderListDto> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    protected async Task SearchOrders()
    {
        await _customerOrderListGrid.ReloadServerData();
    }

    protected void NavEditCustomer()
    {
        NavigationManager.NavigateTo($"/Customers/{CustomerId}/edit");
    }

    protected void NavToOrderDetails(int orderId)
    {
        NavigationManager.NavigateTo($"/Orders/{orderId}");
    }

    protected Color GetCustomerStatusColor(CustomerStatus status)
    {
        return status switch
        {
            CustomerStatus.Active => Color.Success,
            CustomerStatus.Inactive => Color.Error,
            CustomerStatus.NoDoi => Color.Warning,
            _ => Color.Default
        };
    }
}