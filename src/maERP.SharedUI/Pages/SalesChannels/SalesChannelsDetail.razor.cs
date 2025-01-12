using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.SalesChannel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannelsDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ISalesChannelService _salesChannelService { get; set; }

    [Parameter]
    public int salesChannelId { get; set; }

    protected string Title = "Vertriebskanaldetails";

    protected SalesChannelVM salesChannel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (salesChannelId != 0)
        {
            salesChannel = await _salesChannelService.GetSalesChannelDetails(salesChannelId);
        }
    }
}