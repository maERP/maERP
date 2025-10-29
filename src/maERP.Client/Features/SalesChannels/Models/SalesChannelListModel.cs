namespace maERP.Client.Features.SalesChannels.Models;

public partial record SalesChannelListModel
{
    private readonly ISalesChannelsApiClient _salesChannelsApiClient;

    public SalesChannelListModel(ISalesChannelsApiClient salesChannelsApiClient)
    {
        _salesChannelsApiClient = salesChannelsApiClient;
    }
}
