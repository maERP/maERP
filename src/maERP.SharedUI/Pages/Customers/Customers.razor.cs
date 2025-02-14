using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class Customers
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private  MudDataGrid<CustomerListDto> _dataGrid = new();

    private async Task<GridData<CustomerListDto>> LoadGridData(GridState<CustomerListDto> state)
    {
        // TODO: change to CustomerListDto
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateCreated Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<CustomerListDto>>(
            $"/api/v1/Customers?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_searchString}&orderBy={orderBy}")
            ?? new PaginatedResult<CustomerListDto>(new List<CustomerListDto>());

        GridData<CustomerListDto> data = new()
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