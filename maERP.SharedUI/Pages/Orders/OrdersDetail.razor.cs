using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Orders;

public partial class OrdersDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IOrderService _orderService { get; set; }

    [Parameter]
    public int orderId { get; set; }

    protected OrderVM order = new();

    protected override async Task OnParametersSetAsync()
    {
        if (orderId != 0)
        {
            order = await _orderService.GetOrderDetails(orderId);
        }
    }
}