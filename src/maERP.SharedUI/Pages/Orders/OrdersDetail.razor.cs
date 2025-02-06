using maERP.Domain.Dtos.Order;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Orders;

public partial class OrdersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int orderId { get; set; }

    protected OrderDetailDto OrderDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (orderId != 0)
        {
            OrderDetail = await HttpService.GetAsync<OrderDetailDto>($"/api/v1/Orders/{orderId}")
                          ?? new OrderDetailDto();
        }
    }
}