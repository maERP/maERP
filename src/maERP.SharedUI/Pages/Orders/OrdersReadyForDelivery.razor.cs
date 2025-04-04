using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Orders;

public partial class OrdersReadyForDelivery
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private MudDataGrid<OrderListDto> _dataGrid = new();

    private async Task<GridData<OrderListDto>> LoadGridData(GridState<OrderListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateOrdered Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<OrderListDto>>(
            $"/api/v1/Orders/readyfordelivery?pageNumber={pageNumber}&pageSize={pageSize}&orderBy={orderBy}")
            ?? new PaginatedResult<OrderListDto>(new List<OrderListDto>());

        GridData<OrderListDto> data = new()
        {  
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }
} 