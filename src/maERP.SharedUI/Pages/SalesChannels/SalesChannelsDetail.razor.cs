using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.SalesChannel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannelsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ISalesChannelService SalesChannelService { get; set; }

    [Parameter]
    public int salesChannelId { get; set; }

    protected string Title = "Vertriebskanaldetails";

    protected SalesChannelVm SalesChannel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (salesChannelId != 0)
        {
            SalesChannel = await SalesChannelService.GetSalesChannelDetails(salesChannelId);
        }
    }
}