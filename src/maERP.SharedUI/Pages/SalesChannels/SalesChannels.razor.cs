using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.SalesChannel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannels
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ISalesChannelService SalesChannelService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVm> _dataGrid = new();

    private async Task<GridData<SalesChannelVm>> LoadGridData(GridState<SalesChannelVm> state)
    {
        var apiResponse = await SalesChannelService.GetSalesChannels(state.Page, state.PageSize, _searchString);
        GridData<SalesChannelVm> data = new()
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