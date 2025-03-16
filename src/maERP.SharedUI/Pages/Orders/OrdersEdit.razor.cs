using System.Net.Http.Json;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Dtos.Product;
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

    // Product search properties
    public ProductListDto? SelectedProduct { get; set; }
    public int ProductQuantity { get; set; } = 1;
    
    // Customer search properties
    public CustomerListDto? SelectedCustomer { get; set; }
    public CustomerDetailDto? SelectedCustomerDetail { get; set; }
    
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

    // Product search functionality
    private async Task<IEnumerable<ProductListDto>>? SearchProducts(string? searchText, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchText) || searchText.Length < 2)
            return new List<ProductListDto>();

        var response = await HttpService.GetAsync<Result<List<ProductListDto>>>($"/api/v1/Products?searchTerm={Uri.EscapeDataString(searchText)}");
        
        if (response != null && response.Succeeded)
        {
            return response.Data;
        }
        
        return new List<ProductListDto>();
    }

    private void AddProductToOrder()
    {
        if (SelectedProduct == null || ProductQuantity <= 0)
            return;

        // Check if the product is already in the order
        var existingItem = Order.OrderItems.FirstOrDefault(i => i.Name == SelectedProduct.Name && i.ProductId == SelectedProduct.Id);
        
        if (existingItem != null)
        {
            // Update quantity if the product already exists in the order
            existingItem.Quantity += ProductQuantity;
        }
        else
        {
            // Add a new order item
            var newItem = new OrderItem
            {
                ProductId = SelectedProduct.Id,
                Name = SelectedProduct.Name,
                Price = SelectedProduct.Price,
                Quantity = ProductQuantity
            };
            
            Order.OrderItems.Add(newItem);
        }
        
        // Reset selection
        SelectedProduct = null;
        ProductQuantity = 1;
        
        // Recalculate totals
        CalculateTotals();
        
        StateHasChanged();
    }
    
    // Customer search functionality
    private async Task<IEnumerable<CustomerListDto>>? SearchCustomers(string? searchText, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchText) || searchText.Length < 2)
            return new List<CustomerListDto>();

        var response = await HttpService.GetAsync<Result<List<CustomerListDto>>>($"/api/v1/Customers?searchTerm={Uri.EscapeDataString(searchText)}");
        
        if (response != null && response.Succeeded)
        {
            return response.Data;
        }
        
        return new List<CustomerListDto>();
    }
    
    private async Task ApplyCustomerAddresses()
    {
        if (SelectedCustomer == null)
            return;
            
        // Fetch detailed customer information including addresses
        var response = await HttpService.GetAsync<Result<CustomerDetailDto>>($"/api/v1/Customers/{SelectedCustomer.Id}");
        
        if (response == null || !response.Succeeded)
        {
            Snackbar.Add("Kundenadresse konnte nicht geladen werden", Severity.Error);
            return;
        }
        
        SelectedCustomerDetail = response.Data;
        
        if (SelectedCustomerDetail == null || !SelectedCustomerDetail.CustomerAddresses.Any())
        {
            Snackbar.Add("Keine Adressen für diesen Kunden vorhanden", Severity.Warning);
            return;
        }
        
        // Set customer ID in the order
        Order.CustomerId = SelectedCustomerDetail.Id;
        
        // Find default addresses
        var defaultInvoiceAddress = SelectedCustomerDetail.CustomerAddresses.FirstOrDefault(a => a.DefaultInvoiceAddress);
        var defaultDeliveryAddress = SelectedCustomerDetail.CustomerAddresses.FirstOrDefault(a => a.DefaultDeliveryAddress);
        
        // If no default addresses are found, use the first address for both
        if (defaultInvoiceAddress == null && SelectedCustomerDetail.CustomerAddresses.Any())
            defaultInvoiceAddress = SelectedCustomerDetail.CustomerAddresses.First();
            
        if (defaultDeliveryAddress == null && SelectedCustomerDetail.CustomerAddresses.Any())
            defaultDeliveryAddress = SelectedCustomerDetail.CustomerAddresses.First();
        
        // Apply invoice address
        if (defaultInvoiceAddress != null)
        {
            Order.InvoiceAddressFirstName = defaultInvoiceAddress.Firstname;
            Order.InvoiceAddressLastName = defaultInvoiceAddress.Lastname;
            Order.InvoiceAddressCompanyName = defaultInvoiceAddress.CompanyName;
            Order.InvoiceAddressStreet = $"{defaultInvoiceAddress.Street} {defaultInvoiceAddress.HouseNr}".Trim();
            Order.InvoiceAddressZip = defaultInvoiceAddress.Zip;
            Order.InvoiceAddressCity = defaultInvoiceAddress.City;
        }
        
        // Apply delivery address
        if (defaultDeliveryAddress != null)
        {
            Order.DeliveryAddressFirstName = defaultDeliveryAddress.Firstname;
            Order.DeliveryAddressLastName = defaultDeliveryAddress.Lastname;
            Order.DeliveryAddressCompanyName = defaultDeliveryAddress.CompanyName;
            Order.DeliveryAddressStreet = $"{defaultDeliveryAddress.Street} {defaultDeliveryAddress.HouseNr}".Trim();
            Order.DeliverAddressZip = defaultDeliveryAddress.Zip;
            Order.DeliveryAddressCity = defaultDeliveryAddress.City;
        }
        
        Snackbar.Add("Adressen wurden übernommen", Severity.Success);
        StateHasChanged();
    }
}