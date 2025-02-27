using maERP.Domain.Dtos.Order;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Orders;

public partial class OrdersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int orderId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected OrderUpdateDto Order = new();

    protected override async Task OnParametersSetAsync()
    {
        if (orderId != 0)
        {
            Title = "Bearbeiten";
            Order = await HttpService.GetAsync<OrderUpdateDto>($"/api/v1/Orders/{orderId}") ?? new OrderUpdateDto();
        }
    }

    protected async Task Save()
    {
        if (orderId != 0)
        {
            await HttpService.PutAsJsonAsync<OrderUpdateDto>($"/api/v1/Orders/{orderId}", Order);
        }
        else
        {
            await HttpService.PostAsJsonAsync<OrderUpdateDto>("/api/v1/Orders/", Order);
        }
        
        StateHasChanged();
        NavigationManager.NavigateTo("/Orders");
    }
}