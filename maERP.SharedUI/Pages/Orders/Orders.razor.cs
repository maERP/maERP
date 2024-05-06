using maERP.SharedUI.Models.Order;

namespace maERP.SharedUI.Pages.Orders;

public partial class Orders
{

    private ICollection<OrderVM>? orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await _orderService.GetOrders();
    }
}