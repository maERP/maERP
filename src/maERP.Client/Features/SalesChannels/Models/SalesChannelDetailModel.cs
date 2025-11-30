using maERP.Client.Features.SalesChannels.Services;
using maERP.Domain.Dtos.SalesChannel;

namespace maERP.Client.Features.SalesChannels.Models;

/// <summary>
/// Navigation data for SalesChannelDetailModel.
/// </summary>
public record SalesChannelDetailData(Guid SalesChannelId);

/// <summary>
/// Model for sales channel detail page using MVUX pattern.
/// Receives sales channel ID from navigation data.
/// </summary>
public partial record SalesChannelDetailModel
{
    private readonly ISalesChannelService _salesChannelService;
    private readonly INavigator _navigator;
    private readonly Guid _salesChannelId;

    public SalesChannelDetailModel(
        ISalesChannelService salesChannelService,
        INavigator navigator,
        SalesChannelDetailData data)
    {
        _salesChannelService = salesChannelService;
        _navigator = navigator;
        _salesChannelId = data.SalesChannelId;
    }

    /// <summary>
    /// Feed that loads the sales channel details.
    /// </summary>
    public IFeed<SalesChannelDetailDto> SalesChannel => Feed.Async(async ct =>
    {
        var salesChannel = await _salesChannelService.GetSalesChannelAsync(_salesChannelId, ct);
        return salesChannel ?? throw new InvalidOperationException($"Sales channel {_salesChannelId} not found");
    });

    /// <summary>
    /// Navigate to edit sales channel page.
    /// </summary>
    public async Task EditSalesChannel()
    {
        await _navigator.NavigateDataAsync(this, new SalesChannelEditData(_salesChannelId));
    }

    /// <summary>
    /// Navigate back to sales channel list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
