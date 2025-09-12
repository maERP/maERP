using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Orders.ViewModels;

public partial class OrderDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private OrderDetailDto? order;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private Guid orderId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Order != null;

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToOrderEdit { get; set; }

    // Address properties for better UI binding
    public string DeliveryAddress => Order != null
        ? $"{Order.DeliveryAddressCompanyName}{(!string.IsNullOrEmpty(Order.DeliveryAddressCompanyName) ? "\n" : "")}" +
          $"{Order.DeliveryAddressFirstName} {Order.DeliveryAddressLastName}\n" +
          $"{Order.DeliveryAddressStreet}\n" +
          $"{Order.DeliveryAddressZip} {Order.DeliveryAddressCity}\n" +
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

    public OrderDetailViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(Guid orderId)
    {
        OrderId = orderId;
        await LoadOrderAsync();
    }

    [RelayCommand]
    private async Task LoadOrderAsync()
    {
        if (OrderId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<OrderDetailDto>($"orders/{OrderId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Order = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
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
                _debugService.LogInfo($"Loaded order {OrderId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden der Bestellung {OrderId}";
                Order = null;
                _debugService.LogError($"Failed to load order {OrderId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Bestellung: {ex.Message}";
            Order = null;
            _debugService.LogError(ex, $"Exception loading order {OrderId}");
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

    [RelayCommand]
    private async Task EditOrder()
    {
        if (Order != null && NavigateToOrderEdit != null)
        {
            await NavigateToOrderEdit(Order.Id);
        }
    }
}