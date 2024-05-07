using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Orders;

public partial class Orders
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IOrderService _orderService { get; set; }

    private ICollection<OrderVM>? orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await _orderService.GetOrders();
    }
}