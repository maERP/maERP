using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClasses
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<TaxClassListDto> _dataGrid = new();

    private async Task<GridData<TaxClassListDto>> LoadGridData(GridState<TaxClassListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateCreated Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<TaxClassListDto>>(
            $"/api/v1/TaxClasses?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_searchString}&orderBy={orderBy}")
            ?? new PaginatedResult<TaxClassListDto>(new List<TaxClassListDto>());

        GridData<TaxClassListDto> data = new()
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