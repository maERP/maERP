using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
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
    public int orderId { get; set; }

    protected string Title = string.Empty;

    protected OrderDetailDto Order = new();

    protected override async Task OnInitializedAsync()
    {
        if (orderId == 0)
        {
            Title = "Bestellung nicht gefunden";
        }
        else
        {
            Title = $"Bestellung {orderId}";

            var result = await HttpService.GetAsync<Result<OrderDetailDto>>($"/api/v1/Orders/{orderId}");

            if (result != null && result.Succeeded)
            {
                Order = result.Data;
            }
        }
    }
}