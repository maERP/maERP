using System.Net.Http.Json;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Orders;

public partial class OrdersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required OrderInputValidator Validator { get; set; }

    [Parameter]
    public int orderId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public OrderInputDto Order { get; set; } = new()
    {
        DateOrdered = DateTime.Now,
        PaymentStatus = PaymentStatus.Unknown,
        OrderItems = new List<OrderItem>()
    };

    protected override async Task OnInitializedAsync()
    {
        if (orderId == 0)
        {
            Title = "Bestellung hinzufügen";
        }
        else
        {
            Title = $"Bestellung {orderId} bearbeiten";
            
            var result = await HttpService.GetAsync<Result<OrderInputDto>>($"/api/v1/Orders/{orderId}");
            
            if (result != null && result.Succeeded)
            {
                Order = result.Data;
            }
            else
            {
                Snackbar.Add("Bestellung konnte nicht geladen werden", Severity.Error);
                NavigateToList();
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        // Calculate totals before saving
        CalculateTotals();

        if (orderId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/Orders", Order);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/Orders/{orderId}", Order);
        }

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Bestellung gespeichert", Severity.Success);
                NavigateToList();
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add("SERVER: " + errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("Bestellung konnte nicht gespeichert werden", Severity.Error);
        }
    }

    protected void CalculateTotals()
    {
        // Calculate subtotal from order items
        Order.Subtotal = Order.OrderItems.Sum(item => item.Price * (decimal)item.Quantity);
        
        // Calculate total
        Order.Total = Order.Subtotal + Order.ShippingCost + Order.TotalTax;
    }

    protected async Task OnValidSubmit()
    {
        if (Form is not null)
        {
            await Form.Validate();
            
            if (Form.IsValid)
            {
                await Save();
            }
        }
    }
    
    public void NavigateToList()
    {
        NavigationManager.NavigateTo("/Orders");
    }
}