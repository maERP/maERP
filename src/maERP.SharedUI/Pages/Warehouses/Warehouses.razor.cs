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
    public MudDataGrid<WarehouseListDto> DataGrid = new();

    protected override void OnInitialized()
    {
    }

    private async Task<GridData<WarehouseListDto>> LoadGridData(GridState<WarehouseListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0 
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateCreated Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<WarehouseListDto>>(
            $"/api/v1/Warehouses?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_searchString}&orderBy={orderBy}")
            ?? new PaginatedResult<WarehouseListDto>(new List<WarehouseListDto>());
        
        GridData<WarehouseListDto> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search(string searchString)
    {
        _searchString = searchString;

        await DataGrid.ReloadServerData();
    }
}