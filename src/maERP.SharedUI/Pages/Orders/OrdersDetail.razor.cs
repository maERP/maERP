using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Orders;

public partial class OrdersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IOrderService OrderService { get; set; }

    [Parameter]
    public int orderId { get; set; }

    protected OrderVm Order = new();

    protected override async Task OnParametersSetAsync()
    {
        if (orderId != 0)
        {
            Order = await OrderService.GetOrderDetails(orderId);
        }
    }
}