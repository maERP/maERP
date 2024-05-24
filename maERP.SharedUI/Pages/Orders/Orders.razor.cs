using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace maERP.SharedUI.Pages.Orders;

public partial class Orders
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IOrderService _orderService { get; set; }

    private ICollection<OrderListVM>? orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await _orderService.GetOrders();
    }
}