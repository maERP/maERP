using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Invoices;

public partial class InvoicesList
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;
    public MudDataGrid<InvoiceListDto> DataGrid = new();

    protected override void OnInitialized()
    {
    }

    private async Task<GridData<InvoiceListDto>> LoadGridData(GridState<InvoiceListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0 
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "InvoiceDate Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<InvoiceListDto>>(
            $"/api/v1/Invoices?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_searchString}&orderBy={orderBy}")
            ?? new PaginatedResult<InvoiceListDto>(new List<InvoiceListDto>());
        
        GridData<InvoiceListDto> data = new()
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