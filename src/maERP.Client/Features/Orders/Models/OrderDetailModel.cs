using maERP.Client.Features.Orders.Services;
using maERP.Domain.Dtos.Order;

namespace maERP.Client.Features.Orders.Models;

/// <summary>
/// Model for order detail page using MVUX pattern.
/// Receives order ID from navigation data.
/// </summary>
public partial record OrderDetailModel
{
    private readonly IOrderService _orderService;
    private readonly INavigator _navigator;
    private readonly Guid _orderId;

    public OrderDetailModel(
        IOrderService orderService,
        INavigator navigator,
        OrderDetailData data)
    {
        _orderService = orderService;
        _navigator = navigator;
        _orderId = data.orderId;
    }

    /// <summary>
    /// Feed that loads the order details.
    /// </summary>
    public IFeed<OrderDetailDto> Order => Feed.Async(async ct =>
    {
        var order = await _orderService.GetOrderAsync(_orderId, ct);
        return order ?? throw new InvalidOperationException($"Order {_orderId} not found");
    });

    /// <summary>
    /// Navigate back to order list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Navigate to edit order page.
    /// </summary>
    public async Task EditOrder()
    {
        await _navigator.NavigateDataAsync(this, new OrderEditData(_orderId));
    }
}
