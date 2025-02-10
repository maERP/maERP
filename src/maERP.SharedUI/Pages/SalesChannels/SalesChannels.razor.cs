using maERP.Domain.Dtos.Order;
using maERP.Domain.Dtos.ProductSalesChannel;
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
        var apiResponse = await HttpService.GetAsync<PaginatedResult<SalesChannelListDto>>("/api/v1/SalesChannels")
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