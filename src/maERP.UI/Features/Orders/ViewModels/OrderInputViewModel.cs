using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Orders.ViewModels;

public partial class OrderInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid orderId;

    [ObservableProperty]
    [Required(ErrorMessage = "Sales Channel ist erforderlich")]
    [NotifyDataErrorInfo]
    private Guid salesChannelId;

    [ObservableProperty]
    private string remoteOrderId = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Kunde ist erforderlich")]
    [NotifyDataErrorInfo]
    private int customerId;

    [ObservableProperty]
    private OrderStatus status = OrderStatus.Pending;

    [ObservableProperty]
    private ObservableCollection<OrderItem> orderItems = new();

    [ObservableProperty]
    private string paymentMethod = string.Empty;

    [ObservableProperty]
    private PaymentStatus paymentStatus = PaymentStatus.Unknown;

    [ObservableProperty]
    private string paymentProvider = string.Empty;

    [ObservableProperty]
    private string paymentTransactionId = string.Empty;

    [ObservableProperty]
    private string customerNote = string.Empty;

    [ObservableProperty]
    private string internalNote = string.Empty;

    [ObservableProperty]
    private string shippingMethod = string.Empty;

    [ObservableProperty]
    private string shippingStatus = string.Empty;

    [ObservableProperty]
    private string shippingProvider = string.Empty;

    [ObservableProperty]
    private string shippingTrackingId = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private decimal subtotal;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private decimal shippingCost;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private decimal totalTax;

    [ObservableProperty]
    private decimal total;

    [ObservableProperty]
    private string note = string.Empty;

    [ObservableProperty]
    private string deliveryAddressFirstName = string.Empty;

    [ObservableProperty]
    private string deliveryAddressLastName = string.Empty;

    [ObservableProperty]
    private string deliveryAddressCompanyName = string.Empty;

    [ObservableProperty]
    private string deliveryAddressPhone = string.Empty;

    [ObservableProperty]
    private string deliveryAddressStreet = string.Empty;

    [ObservableProperty]
    private string deliveryAddressCity = string.Empty;

    [ObservableProperty]
    private string deliveryAddressZip = string.Empty;

    [ObservableProperty]
    private string deliveryAddressCountry = string.Empty;

    [ObservableProperty]
    private string invoiceAddressFirstName = string.Empty;

    [ObservableProperty]
    private string invoiceAddressLastName = string.Empty;

    [ObservableProperty]
    private string invoiceAddressCompanyName = string.Empty;

    [ObservableProperty]
    private string invoiceAddressPhone = string.Empty;

    [ObservableProperty]
    private string invoiceAddressStreet = string.Empty;

    [ObservableProperty]
    private string invoiceAddressCity = string.Empty;

    [ObservableProperty]
    private string invoiceAddressZip = string.Empty;

    [ObservableProperty]
    private string invoiceAddressCountry = string.Empty;

    [ObservableProperty]
    private DateTime dateOrdered = DateTime.Now;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    // OrderItem management
    [ObservableProperty]
    private bool isAddingOrderItem;

    [ObservableProperty]
    private bool isEditingOrderItem;

    [ObservableProperty]
    private OrderItem? selectedOrderItem;

    [ObservableProperty]
    private Guid newItemProductId;

    [ObservableProperty]
    private double newItemQuantity = 1;

    [ObservableProperty]
    private string newItemName = string.Empty;

    [ObservableProperty]
    private decimal newItemPrice;

    [ObservableProperty]
    private double newItemTaxRate = 19.0;

    public bool IsEditMode => OrderId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"Bestellung #{OrderId} bearbeiten" : "Neue Bestellung erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    // Available options for dropdowns
    public List<OrderStatus> AvailableOrderStatuses { get; } = Enum.GetValues<OrderStatus>().ToList();
    public List<PaymentStatus> AvailablePaymentStatuses { get; } = Enum.GetValues<PaymentStatus>().ToList();

    // Computed properties
    public decimal OrderItemsSubtotal => OrderItems.Sum(item => item.Price * (decimal)item.Quantity);
    public bool HasOrderItems => OrderItems.Any();

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToOrderDetail { get; set; }

    public OrderInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        OrderItems.CollectionChanged += (_, _) => OnPropertyChanged(nameof(OrderItemsSubtotal));
        OrderItems.CollectionChanged += (_, _) => OnPropertyChanged(nameof(HasOrderItems));
        OrderItems.CollectionChanged += (_, _) => CalculateTotals();
    }

    partial void OnSubtotalChanged(decimal value) => CalculateTotals();
    partial void OnShippingCostChanged(decimal value) => CalculateTotals();
    partial void OnTotalTaxChanged(decimal value) => CalculateTotals();

    private void CalculateTotals()
    {
        Total = Subtotal + ShippingCost + TotalTax;
    }

    public async Task InitializeAsync(Guid orderId = default)
    {
        OrderId = orderId;

        if (IsEditMode)
        {
            await LoadAsync();
        }
        else
        {
            ClearForm();
        }
    }

    [RelayCommand]
    private async Task LoadAsync()
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
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var order = result.Data;

                // Map order data to form fields
                SalesChannelId = order.SalesChannelId;
                RemoteOrderId = order.RemoteOrderId;
                CustomerId = order.CustomerId;
                Status = order.Status;
                PaymentMethod = order.PaymentMethod;
                PaymentStatus = order.PaymentStatus;
                PaymentProvider = order.PaymentProvider;
                PaymentTransactionId = order.PaymentTransactionId;
                CustomerNote = string.Empty; // Not available in OrderDetailDto
                InternalNote = string.Empty; // Not available in OrderDetailDto
                ShippingMethod = order.ShippingMethod;
                ShippingStatus = order.ShippingStatus;
                ShippingProvider = order.ShippingProvider;
                ShippingTrackingId = order.ShippingTrackingId;
                Subtotal = order.Subtotal;
                ShippingCost = order.ShippingCost;
                TotalTax = order.TotalTax;
                Total = order.Total;
                Note = order.Note;
                DeliveryAddressFirstName = order.DeliveryAddressFirstName;
                DeliveryAddressLastName = order.DeliveryAddressLastName;
                DeliveryAddressCompanyName = order.DeliveryAddressCompanyName;
                DeliveryAddressPhone = order.DeliveryAddressPhone;
                DeliveryAddressStreet = order.DeliveryAddressStreet;
                DeliveryAddressCity = order.DeliveryAddressCity;
                DeliveryAddressZip = order.DeliveryAddressZip;
                DeliveryAddressCountry = order.DeliveryAddressCountry;
                InvoiceAddressFirstName = order.InvoiceAddressFirstName;
                InvoiceAddressLastName = order.InvoiceAddressLastName;
                InvoiceAddressCompanyName = order.InvoiceAddressCompanyName;
                InvoiceAddressPhone = order.InvoiceAddressPhone;
                InvoiceAddressStreet = order.InvoiceAddressStreet;
                InvoiceAddressCity = order.InvoiceAddressCity;
                InvoiceAddressZip = order.InvoiceAddressZip;
                InvoiceAddressCountry = order.InvoiceAddressCountry;
                DateOrdered = order.DateOrdered;

                // Load order items
                OrderItems.Clear();
                if (order.OrderItems != null)
                {
                    foreach (var item in order.OrderItems)
                    {
                        OrderItems.Add(item);
                    }
                }

                _debugService.LogInfo($"Loaded order {OrderId} for editing successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden der Bestellung {OrderId}";
                _debugService.LogError($"Failed to load order {OrderId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Bestellung: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading order {OrderId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!ValidateForm()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var orderDto = new OrderInputDto
            {
                Id = OrderId,
                SalesChannelId = SalesChannelId,
                RemoteOrderId = RemoteOrderId,
                CustomerId = CustomerId,
                Status = Status,
                OrderItems = OrderItems.ToList(),
                PaymentMethod = PaymentMethod,
                PaymentStatus = PaymentStatus,
                PaymentProvider = PaymentProvider,
                PaymentTransactionId = PaymentTransactionId,
                CustomerNote = CustomerNote,
                InternalNote = InternalNote,
                ShippingMethod = ShippingMethod,
                ShippingStatus = ShippingStatus,
                ShippingProvider = ShippingProvider,
                ShippingTrackingId = ShippingTrackingId,
                Subtotal = Subtotal,
                ShippingCost = ShippingCost,
                TotalTax = TotalTax,
                Total = Total,
                Note = Note,
                DeliveryAddressFirstName = DeliveryAddressFirstName,
                DeliveryAddressLastName = DeliveryAddressLastName,
                DeliveryAddressCompanyName = DeliveryAddressCompanyName,
                DeliveryAddressPhone = DeliveryAddressPhone,
                DeliveryAddressStreet = DeliveryAddressStreet,
                DeliveryAddressCity = DeliveryAddressCity,
                DeliveryAddressZip = DeliveryAddressZip,
                DeliveryAddressCountry = DeliveryAddressCountry,
                InvoiceAddressFirstName = InvoiceAddressFirstName,
                InvoiceAddressLastName = InvoiceAddressLastName,
                InvoiceAddressCompanyName = InvoiceAddressCompanyName,
                InvoiceAddressPhone = InvoiceAddressPhone,
                InvoiceAddressStreet = InvoiceAddressStreet,
                InvoiceAddressCity = InvoiceAddressCity,
                InvoiceAddressZip = InvoiceAddressZip,
                InvoiceAddressCountry = InvoiceAddressCountry,
                DateOrdered = DateOrdered
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<OrderInputDto, Guid>($"orders/{OrderId}", orderDto)
                : await _httpService.PostAsync<OrderInputDto, Guid>("orders", orderDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Order {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToOrderDetail != null)
                {
                    await NavigateToOrderDetail(OrderId);
                }
                else if (!IsEditMode && NavigateToOrderDetail != null)
                {
                    var createdOrderId = result.Data;
                    if (createdOrderId != Guid.Empty)
                    {
                        await NavigateToOrderDetail(createdOrderId);
                    }
                    else
                    {
                        GoBack();
                    }
                }
                else
                {
                    GoBack();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Speichern" : "Erstellen")} der Bestellung";
                _debugService.LogError($"Failed to save order: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving order");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoBack();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    private void ClearForm()
    {
        SalesChannelId = Guid.Empty;
        RemoteOrderId = string.Empty;
        CustomerId = 0;
        Status = OrderStatus.Pending;
        OrderItems.Clear();
        PaymentMethod = string.Empty;
        PaymentStatus = PaymentStatus.Unknown;
        PaymentProvider = string.Empty;
        PaymentTransactionId = string.Empty;
        CustomerNote = string.Empty;
        InternalNote = string.Empty;
        ShippingMethod = string.Empty;
        ShippingStatus = string.Empty;
        ShippingProvider = string.Empty;
        ShippingTrackingId = string.Empty;
        Subtotal = 0;
        ShippingCost = 0;
        TotalTax = 0;
        Total = 0;
        Note = string.Empty;
        DeliveryAddressFirstName = string.Empty;
        DeliveryAddressLastName = string.Empty;
        DeliveryAddressCompanyName = string.Empty;
        DeliveryAddressPhone = string.Empty;
        DeliveryAddressStreet = string.Empty;
        DeliveryAddressCity = string.Empty;
        DeliveryAddressZip = string.Empty;
        DeliveryAddressCountry = string.Empty;
        InvoiceAddressFirstName = string.Empty;
        InvoiceAddressLastName = string.Empty;
        InvoiceAddressCompanyName = string.Empty;
        InvoiceAddressPhone = string.Empty;
        InvoiceAddressStreet = string.Empty;
        InvoiceAddressCity = string.Empty;
        InvoiceAddressZip = string.Empty;
        InvoiceAddressCountry = string.Empty;
        DateOrdered = DateTime.Now;
        ErrorMessage = string.Empty;

        ClearErrors();
    }

    private bool ValidateForm()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Eingabefehler";
            return false;
        }
        return true;
    }

    // OrderItem management commands
    [RelayCommand]
    private void StartAddingOrderItem()
    {
        IsAddingOrderItem = true;
        IsEditingOrderItem = false;
        ClearOrderItemForm();
    }

    [RelayCommand]
    private void StartEditingOrderItem(OrderItem? item)
    {
        if (item == null) return;

        SelectedOrderItem = item;
        IsEditingOrderItem = true;
        IsAddingOrderItem = true; // Show the form

        // Fill form with existing item data
        NewItemProductId = item.ProductId;
        NewItemQuantity = item.Quantity;
        NewItemName = item.Name;
        NewItemPrice = item.Price;
        NewItemTaxRate = item.TaxRate;
    }

    [RelayCommand]
    private void SaveOrderItem()
    {
        if (IsEditingOrderItem && SelectedOrderItem != null)
        {
            // Update existing item
            SelectedOrderItem.ProductId = NewItemProductId;
            SelectedOrderItem.Quantity = NewItemQuantity;
            SelectedOrderItem.Name = NewItemName;
            SelectedOrderItem.Price = NewItemPrice;
            SelectedOrderItem.TaxRate = NewItemTaxRate;
        }
        else if (IsAddingOrderItem)
        {
            // Create new item
            var newItem = new OrderItem
            {
                Id = Guid.Empty, // Will be set by server
                OrderId = OrderId,
                ProductId = NewItemProductId,
                Quantity = NewItemQuantity,
                Name = NewItemName,
                Price = NewItemPrice,
                TaxRate = NewItemTaxRate
            };

            OrderItems.Add(newItem);
        }

        CancelOrderItemEdit();
    }

    [RelayCommand]
    private void CancelOrderItemEdit()
    {
        IsAddingOrderItem = false;
        IsEditingOrderItem = false;
        SelectedOrderItem = null;
        ClearOrderItemForm();
    }

    [RelayCommand]
    private void DeleteOrderItem(OrderItem? item)
    {
        if (item == null) return;

        OrderItems.Remove(item);

        if (SelectedOrderItem == item)
        {
            CancelOrderItemEdit();
        }
    }

    private void ClearOrderItemForm()
    {
        NewItemProductId = Guid.Empty;
        NewItemQuantity = 1;
        NewItemName = string.Empty;
        NewItemPrice = 0;
        NewItemTaxRate = 19.0;
    }

    [RelayCommand]
    private void CopyDeliveryToInvoice()
    {
        InvoiceAddressFirstName = DeliveryAddressFirstName;
        InvoiceAddressLastName = DeliveryAddressLastName;
        InvoiceAddressCompanyName = DeliveryAddressCompanyName;
        InvoiceAddressPhone = DeliveryAddressPhone;
        InvoiceAddressStreet = DeliveryAddressStreet;
        InvoiceAddressCity = DeliveryAddressCity;
        InvoiceAddressZip = DeliveryAddressZip;
        InvoiceAddressCountry = DeliveryAddressCountry;
    }

    [RelayCommand]
    private void CopyInvoiceToDelivery()
    {
        DeliveryAddressFirstName = InvoiceAddressFirstName;
        DeliveryAddressLastName = InvoiceAddressLastName;
        DeliveryAddressCompanyName = InvoiceAddressCompanyName;
        DeliveryAddressPhone = InvoiceAddressPhone;
        DeliveryAddressStreet = InvoiceAddressStreet;
        DeliveryAddressCity = InvoiceAddressCity;
        DeliveryAddressZip = InvoiceAddressZip;
        DeliveryAddressCountry = InvoiceAddressCountry;
    }
}
