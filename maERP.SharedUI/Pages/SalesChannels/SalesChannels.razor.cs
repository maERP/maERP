using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.SalesChannel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannels
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ISalesChannelService _salesChannelService { get; set; }

    private ICollection<SalesChannelVM>? salesChannels;

    protected override async Task OnInitializedAsync()
    {
        salesChannels = await _salesChannelService.GetSalesChannels();
    }
}