using maERP.SharedUI.Models.SalesChannel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannelsDetail
{

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