using maERP.Domain.Dtos.SalesChannel;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannelsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int salesChannelId { get; set; }

    protected string Title = "Vertriebskanaldetails";

    protected SalesChannelDetailDto SalesChannel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (salesChannelId != 0)
        {
            SalesChannel = await HttpService.GetAsync<SalesChannelDetailDto>($"/api/v1/SalesChannels/{salesChannelId}")
                           ?? new SalesChannelDetailDto();
        }
    }
}