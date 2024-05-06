using maERP.SharedUI.Models.SalesChannel;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannels
{

    private ICollection<SalesChannelVM>? salesChannels;

    protected override async Task OnInitializedAsync()
    {
        salesChannels = await _salesChannelService.GetSalesChannels();
    }
}