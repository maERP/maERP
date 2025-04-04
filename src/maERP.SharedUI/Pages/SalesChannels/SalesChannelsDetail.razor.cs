using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
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

    protected SalesChannelDetailDto SalesChannelDetail { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        if (salesChannelId != 0)
        {
            var result = await HttpService.GetAsync<Result<SalesChannelDetailDto>>($"/api/v1/SalesChannels/{salesChannelId}");
            
            if (result != null && result.Succeeded)
            {
                SalesChannelDetail = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                Title = "nicht gefunden";
            }
        }
        else 
        {
            Title = "nicht gefunden";
        }
    }
}