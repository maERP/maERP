using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannels
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ISalesChannelService _salesChannelService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVM> _dataGrid = new();

    private async Task<GridData<SalesChannelVM>> LoadGridData(GridState<SalesChannelVM> state)
    {
        var apiResponse = await _salesChannelService.GetSalesChannels(state.Page, state.PageSize, _searchString);
        GridData<SalesChannelVM> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        if (_dataGrid is not null)
        {
            await _dataGrid!.ReloadServerData();
        }
    }
}