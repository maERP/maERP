using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannels
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<SalesChannelListDto> _dataGrid = new();

    private async Task<GridData<SalesChannelListDto>> LoadGridData(GridState<SalesChannelListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateCreated Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<SalesChannelListDto>>(
            $"/api/v1/SalesChannels?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_searchString}&orderBy={orderBy}")
            ?? new PaginatedResult<SalesChannelListDto>(new List<SalesChannelListDto>());

        GridData<SalesChannelListDto> data = new()
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