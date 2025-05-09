using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
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
    public int OrderId { get; set; }

    private string _title = string.Empty;

    private OrderDetailDto _order = new();

    protected override async Task OnInitializedAsync()
    {
        if (OrderId == 0)
        {
            _title = "Bestellung nicht gefunden";
        }
        else
        {
            var result = await HttpService.GetAsync<Result<OrderDetailDto>>($"/api/v1/Orders/{OrderId}");

            if (result != null && result.Succeeded)
            {
                var orderHistory =
                    await HttpService.GetAsync<List<OrderHistoryDto>>($"/api/v1/Orders/{OrderId}/History") ??
                    new List<OrderHistoryDto>();
                _order.OrderHistory = orderHistory;
                
                _title = $"Bestellung {OrderId}";
                _order = result.Data;
            }
        }
    }
}