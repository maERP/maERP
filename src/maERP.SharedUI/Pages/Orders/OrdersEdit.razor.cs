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

    protected OrderEditDto Order = new();

    protected override async Task OnParametersSetAsync()
    {
        if (orderId != 0)
        {
            Title = "Bearbeiten";
            Order = await HttpService.GetAsync<OrderEditDto>($"/api/v1/Orders/{orderId}") ?? new OrderEditDto();
        }
    }

    protected async Task Save()
    {
        if (orderId != 0)
        {
            await HttpService.PutAsJsonAsync<OrderEditDto>($"/api/v1/Orders/{orderId}", Order);
        }
        else
        {
            await HttpService.PostAsJsonAsync<OrderEditDto>("/api/v1/Orders/", Order);
        }
        
        StateHasChanged();
        NavigationManager.NavigateTo("/Orders");
    }
}