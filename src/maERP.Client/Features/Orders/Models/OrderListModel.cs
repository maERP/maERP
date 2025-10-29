namespace maERP.Client.Features.Orders.Models;

public partial record OrderListModel
{
    private readonly IOrdersApiClient _ordersApiClient;

    public OrderListModel(IOrdersApiClient ordersApiClient)
    {
        _ordersApiClient = ordersApiClient;
    }
}
