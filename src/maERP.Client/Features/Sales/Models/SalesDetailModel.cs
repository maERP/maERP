using maERP.Client.Features.Saless.Services;
using maERP.Domain.Dtos.Sales;

namespace maERP.Client.Features.Saless.Models;

/// <summary>
/// Model for sales detail page using MVUX pattern.
/// Receives sales ID from navigation data.
/// </summary>
public partial record SalesDetailModel
{
    private readonly ISalesService _salesService;
    private readonly INavigator _navigator;
    private readonly Guid _salesId;

    public SalesDetailModel(
        ISalesService salesService,
        INavigator navigator,
        SalesDetailData data)
    {
        _salesService = salesService;
        _navigator = navigator;
        _salesId = data.salesId;
    }

    /// <summary>
    /// Feed that loads the sales details.
    /// </summary>
    public IFeed<SalesDetailDto> Sales => Feed.Async(async ct =>
    {
        var sales = await _salesService.GetSalesAsync(_salesId, ct);
        return sales ?? throw new InvalidOperationException($"Sales {_salesId} not found");
    });

    /// <summary>
    /// Navigate back to sales list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Navigate to edit sales page.
    /// </summary>
    public async Task EditSales()
    {
        await _navigator.NavigateDataAsync(this, new SalesEditData(_salesId));
    }
}
