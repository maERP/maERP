using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Orders;

public partial class Orders
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private readonly MudDataGrid<OrderListDto> _dataGrid = new();

    private async Task<GridData<OrderListDto>> LoadGridData(GridState<OrderListDto> state)
    {
        var apiResponse = await HttpService.GetAsync<PaginatedResult<OrderListDto>>("/api/v1/Orders")
                          ?? new PaginatedResult<OrderListDto>(new List<OrderListDto>());
        
        GridData<OrderListDto> data = new()
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