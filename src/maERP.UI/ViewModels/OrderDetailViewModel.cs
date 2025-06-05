using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class OrderDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    private OrderDetailDto? order;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int orderId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Order != null;

    public Action? GoBackAction { get; set; }

    // Address properties for better UI binding
    public string DeliveryAddress => Order != null
        ? $"{Order.DeliveryAddressCompanyName}{(!string.IsNullOrEmpty(Order.DeliveryAddressCompanyName) ? "\n" : "")}" +
          $"{Order.DeliveryAddressFirstName} {Order.DeliveryAddressLastName}\n" +
          $"{Order.DeliveryAddressStreet}\n" +
          $"{Order.DeliverAddressZip} {Order.DeliveryAddressCity}\n" +
          $"{Order.DeliveryAddressCountry}" +
          (!string.IsNullOrEmpty(Order.DeliveryAddressPhone) ? $"\nTel: {Order.DeliveryAddressPhone}" : "")
        : string.Empty;

    public string InvoiceAddress => Order != null
        ? $"{Order.InvoiceAddressCompanyName}{(!string.IsNullOrEmpty(Order.InvoiceAddressCompanyName) ? "\n" : "")}" +
          $"{Order.InvoiceAddressFirstName} {Order.InvoiceAddressLastName}\n" +
          $"{Order.InvoiceAddressStreet}\n" +
          $"{Order.InvoiceAddressZip} {Order.InvoiceAddressCity}\n" +
          $"{Order.InvoiceAddressCountry}" +
          (!string.IsNullOrEmpty(Order.InvoiceAddressPhone) ? $"\nTel: {Order.InvoiceAddressPhone}" : "")
        : string.Empty;

    // Computed properties for better display
    public decimal OrderItemsTotal => Order?.OrderItems?.Sum(item => item.Price * (decimal)item.Quantity) ?? 0;
    public decimal TaxAmount => Order?.TotalTax ?? 0;
    public bool HasDeliveryAddress => Order != null && (!string.IsNullOrEmpty(Order.DeliveryAddressFirstName) || !string.IsNullOrEmpty(Order.DeliveryAddressLastName));
    public bool HasNote => Order != null && !string.IsNullOrEmpty(Order.Note);
    public bool HasOrderHistory => Order?.OrderHistory?.Any() == true;

    public OrderDetailViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(int orderId)
    {
        OrderId = orderId;
        await LoadOrderAsync();
    }

    [RelayCommand]
    private async Task LoadOrderAsync()
    {
        if (OrderId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<OrderDetailDto>($"orders/{OrderId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Order = null;
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Order = result.Data;
                OnPropertyChanged(nameof(DeliveryAddress));
                OnPropertyChanged(nameof(InvoiceAddress));
                OnPropertyChanged(nameof(OrderItemsTotal));
                OnPropertyChanged(nameof(TaxAmount));
                OnPropertyChanged(nameof(HasDeliveryAddress));
                OnPropertyChanged(nameof(HasNote));
                OnPropertyChanged(nameof(HasOrderHistory));
                System.Diagnostics.Debug.WriteLine($"Loaded order {OrderId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden der Bestellung {OrderId}";
                Order = null;
                System.Diagnostics.Debug.WriteLine($"Failed to load order {OrderId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Bestellung: {ex.Message}";
            Order = null;
            System.Diagnostics.Debug.WriteLine($"Exception loading order {OrderId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadOrderAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }
}