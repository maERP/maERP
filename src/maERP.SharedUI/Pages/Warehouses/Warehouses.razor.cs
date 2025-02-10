using System.Net;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class Warehouses
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<WarehouseListDto> _dataGrid = new();

    private async Task<GridData<WarehouseListDto>> LoadGridData(GridState<WarehouseListDto> state)
    {
        var apiResponse = await HttpService.GetAsync<PaginatedResult<WarehouseListDto>>("/api/v1/Warehouses")
                          ?? new PaginatedResult<WarehouseListDto>(new List<WarehouseListDto>());
        
        GridData<WarehouseListDto> data = new()
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