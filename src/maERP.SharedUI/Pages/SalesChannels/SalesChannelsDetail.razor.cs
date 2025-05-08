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
    public int SalesChannelId { get; set; }

    private string _title = "Vertriebskanaldetails";

    private SalesChannelDetailDto _salesChannelDetail { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        if (SalesChannelId != 0)
        {
            var result = await HttpService.GetAsync<Result<SalesChannelDetailDto>>($"/api/v1/SalesChannels/{SalesChannelId}");
            
            if (result != null && result.Succeeded)
            {
                _title = $"Vertriebskanal - {_salesChannelDetail.Name}";
                _salesChannelDetail = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                _title = "nicht gefunden";
            }
        }
        else 
        {
            _title = "nicht gefunden";
        }
    }
}