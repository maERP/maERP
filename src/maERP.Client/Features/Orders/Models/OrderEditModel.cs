using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Orders.Services;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Orders.Models;

/// <summary>
/// Model for order edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class OrderEditModel : AsyncInitializableModel
{
    private readonly IOrderService _orderService;
    private readonly ISalesChannelService _salesChannelService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _orderId;

    // Order Information
    private int _orderIdNumber;
    private string _remoteOrderId = string.Empty;
    private Guid _salesChannelId;
    private int _customerId;

    // Status
    private OrderStatus _status = OrderStatus.Pending;
    private PaymentStatus _paymentStatus = PaymentStatus.Unknown;

    // Payment Information
    private string _paymentMethod = string.Empty;
    private string _paymentProvider = string.Empty;
    private string _paymentTransactionId = string.Empty;

    // Notes
    private string _customerNote = string.Empty;
    private string _internalNote = string.Empty;

    // Financial Information
    private decimal _subtotal;
    private decimal _shippingCost;
    private decimal _totalTax;
    private decimal _total;

    // Delivery Address
    private string _deliveryAddressFirstName = string.Empty;
    private string _deliveryAddressLastName = string.Empty;
    private string _deliveryAddressCompanyName = string.Empty;
    private string _deliveryAddressPhone = string.Empty;
    private string _deliveryAddressStreet = string.Empty;
    private string _deliveryAddressCity = string.Empty;
    private string _deliveryAddressZip = string.Empty;
    private string _deliveryAddressCountry = string.Empty;

    // Invoice Address
    private string _invoiceAddressFirstName = string.Empty;
    private string _invoiceAddressLastName = string.Empty;
    private string _invoiceAddressCompanyName = string.Empty;
    private string _invoiceAddressPhone = string.Empty;
    private string _invoiceAddressStreet = string.Empty;
    private string _invoiceAddressCity = string.Empty;
    private string _invoiceAddressZip = string.Empty;
    private string _invoiceAddressCountry = string.Empty;

    // Notification Flags
    private bool _orderConfirmationSent;
    private bool _invoiceSent;
    private bool _shippingInformationSent;

    // Date
    private DateTime _dateOrdered = DateTime.UtcNow;

    // Sales Channels
    private ObservableCollection<SalesChannelListDto> _salesChannels = new();

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public OrderEditModel(
        IOrderService orderService,
        ISalesChannelService salesChannelService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<OrderEditModel> logger,
        OrderEditData? data = null)
        : base(logger)
    {
        _orderService = orderService;
        _salesChannelService = salesChannelService;
        _navigator = navigator;
        _localizer = localizer;
        _orderId = data?.orderId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        await LoadSalesChannelsAsync(ct);

        if (_orderId.HasValue)
        {
            await LoadOrderAsync(ct);
        }
    }

    private async Task LoadSalesChannelsAsync(CancellationToken ct)
    {
        var parameters = new QueryParameters { PageSize = 1000 };
        var response = await _salesChannelService.GetSalesChannelsAsync(parameters, ct);
        SalesChannels.Clear();
        if (response?.Data != null)
        {
            foreach (var salesChannel in response.Data)
            {
                SalesChannels.Add(salesChannel);
            }
        }
    }

    public bool IsEditMode => _orderId.HasValue;

    public string Title => IsEditMode
        ? _localizer["OrderEditPage.TitleEdit"]
        : _localizer["OrderEditPage.TitleNew"];

    /// <summary>
    /// Available order status options for the ComboBox.
    /// </summary>
    public IReadOnlyList<OrderStatusOption> OrderStatusOptions { get; } = new List<OrderStatusOption>
    {
        new(OrderStatus.Unknown, "OrderStatus.Unknown"),
        new(OrderStatus.Pending, "OrderStatus.Pending"),
        new(OrderStatus.Processing, "OrderStatus.Processing"),
        new(OrderStatus.ReadyForDelivery, "OrderStatus.ReadyForDelivery"),
        new(OrderStatus.PartiallyDelivered, "OrderStatus.PartiallyDelivered"),
        new(OrderStatus.Completed, "OrderStatus.Completed"),
        new(OrderStatus.Cancelled, "OrderStatus.Cancelled"),
        new(OrderStatus.Returned, "OrderStatus.Returned"),
        new(OrderStatus.Refunded, "OrderStatus.Refunded"),
        new(OrderStatus.OnHold, "OrderStatus.OnHold"),
        new(OrderStatus.Failed, "OrderStatus.Failed")
    };

    /// <summary>
    /// Available payment status options for the ComboBox.
    /// </summary>
    public IReadOnlyList<PaymentStatusOption> PaymentStatusOptions { get; } = new List<PaymentStatusOption>
    {
        new(PaymentStatus.Unknown, "PaymentStatus.Unknown"),
        new(PaymentStatus.Invoiced, "PaymentStatus.Invoiced"),
        new(PaymentStatus.PartiallyPaid, "PaymentStatus.PartiallyPaid"),
        new(PaymentStatus.CompletelyPaid, "PaymentStatus.CompletelyPaid"),
        new(PaymentStatus.FirstReminder, "PaymentStatus.FirstReminder"),
        new(PaymentStatus.SecondReminder, "PaymentStatus.SecondReminder"),
        new(PaymentStatus.ThirdReminder, "PaymentStatus.ThirdReminder"),
        new(PaymentStatus.Encashment, "PaymentStatus.Encashment"),
        new(PaymentStatus.Reserved, "PaymentStatus.Reserved"),
        new(PaymentStatus.Delayed, "PaymentStatus.Delayed"),
        new(PaymentStatus.ReCrediting, "PaymentStatus.ReCrediting"),
        new(PaymentStatus.ReviewNecessary, "PaymentStatus.ReviewNecessary"),
        new(PaymentStatus.NoCreditApproved, "PaymentStatus.NoCreditApproved"),
        new(PaymentStatus.CreditPreliminarilyAccepted, "PaymentStatus.CreditPreliminarilyAccepted")
    };

    #region Order Information

    public int OrderIdNumber
    {
        get => _orderIdNumber;
        set => SetProperty(ref _orderIdNumber, value);
    }

    public string RemoteOrderId
    {
        get => _remoteOrderId;
        set => SetProperty(ref _remoteOrderId, value);
    }

    public Guid SalesChannelId
    {
        get => _salesChannelId;
        set => SetProperty(ref _salesChannelId, value);
    }

    public int CustomerId
    {
        get => _customerId;
        set => SetProperty(ref _customerId, value);
    }

    #endregion

    #region Status

    public OrderStatus Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public PaymentStatus PaymentStatus
    {
        get => _paymentStatus;
        set => SetProperty(ref _paymentStatus, value);
    }

    #endregion

    #region Payment Information

    public string PaymentMethod
    {
        get => _paymentMethod;
        set => SetProperty(ref _paymentMethod, value);
    }

    public string PaymentProvider
    {
        get => _paymentProvider;
        set => SetProperty(ref _paymentProvider, value);
    }

    public string PaymentTransactionId
    {
        get => _paymentTransactionId;
        set => SetProperty(ref _paymentTransactionId, value);
    }

    #endregion

    #region Notes

    public string CustomerNote
    {
        get => _customerNote;
        set => SetProperty(ref _customerNote, value);
    }

    public string InternalNote
    {
        get => _internalNote;
        set => SetProperty(ref _internalNote, value);
    }

    #endregion

    #region Financial Information

    public decimal Subtotal
    {
        get => _subtotal;
        set
        {
            if (SetProperty(ref _subtotal, value))
            {
                CalculateTotal();
            }
        }
    }

    public decimal ShippingCost
    {
        get => _shippingCost;
        set
        {
            if (SetProperty(ref _shippingCost, value))
            {
                CalculateTotal();
            }
        }
    }

    public decimal TotalTax
    {
        get => _totalTax;
        set
        {
            if (SetProperty(ref _totalTax, value))
            {
                CalculateTotal();
            }
        }
    }

    public decimal Total
    {
        get => _total;
        set => SetProperty(ref _total, value);
    }

    private void CalculateTotal()
    {
        Total = Subtotal + ShippingCost + TotalTax;
    }

    #endregion

    #region Delivery Address

    public string DeliveryAddressFirstName
    {
        get => _deliveryAddressFirstName;
        set => SetProperty(ref _deliveryAddressFirstName, value);
    }

    public string DeliveryAddressLastName
    {
        get => _deliveryAddressLastName;
        set => SetProperty(ref _deliveryAddressLastName, value);
    }

    public string DeliveryAddressCompanyName
    {
        get => _deliveryAddressCompanyName;
        set => SetProperty(ref _deliveryAddressCompanyName, value);
    }

    public string DeliveryAddressPhone
    {
        get => _deliveryAddressPhone;
        set => SetProperty(ref _deliveryAddressPhone, value);
    }

    public string DeliveryAddressStreet
    {
        get => _deliveryAddressStreet;
        set => SetProperty(ref _deliveryAddressStreet, value);
    }

    public string DeliveryAddressCity
    {
        get => _deliveryAddressCity;
        set => SetProperty(ref _deliveryAddressCity, value);
    }

    public string DeliveryAddressZip
    {
        get => _deliveryAddressZip;
        set => SetProperty(ref _deliveryAddressZip, value);
    }

    public string DeliveryAddressCountry
    {
        get => _deliveryAddressCountry;
        set => SetProperty(ref _deliveryAddressCountry, value);
    }

    #endregion

    #region Invoice Address

    public string InvoiceAddressFirstName
    {
        get => _invoiceAddressFirstName;
        set => SetProperty(ref _invoiceAddressFirstName, value);
    }

    public string InvoiceAddressLastName
    {
        get => _invoiceAddressLastName;
        set => SetProperty(ref _invoiceAddressLastName, value);
    }

    public string InvoiceAddressCompanyName
    {
        get => _invoiceAddressCompanyName;
        set => SetProperty(ref _invoiceAddressCompanyName, value);
    }

    public string InvoiceAddressPhone
    {
        get => _invoiceAddressPhone;
        set => SetProperty(ref _invoiceAddressPhone, value);
    }

    public string InvoiceAddressStreet
    {
        get => _invoiceAddressStreet;
        set => SetProperty(ref _invoiceAddressStreet, value);
    }

    public string InvoiceAddressCity
    {
        get => _invoiceAddressCity;
        set => SetProperty(ref _invoiceAddressCity, value);
    }

    public string InvoiceAddressZip
    {
        get => _invoiceAddressZip;
        set => SetProperty(ref _invoiceAddressZip, value);
    }

    public string InvoiceAddressCountry
    {
        get => _invoiceAddressCountry;
        set => SetProperty(ref _invoiceAddressCountry, value);
    }

    #endregion

    #region Notification Flags

    public bool OrderConfirmationSent
    {
        get => _orderConfirmationSent;
        set => SetProperty(ref _orderConfirmationSent, value);
    }

    public bool InvoiceSent
    {
        get => _invoiceSent;
        set => SetProperty(ref _invoiceSent, value);
    }

    public bool ShippingInformationSent
    {
        get => _shippingInformationSent;
        set => SetProperty(ref _shippingInformationSent, value);
    }

    #endregion

    #region Date

    public DateTime DateOrdered
    {
        get => _dateOrdered;
        set => SetProperty(ref _dateOrdered, value);
    }

    public DateTimeOffset DateOrderedOffset
    {
        get => new DateTimeOffset(DateOrdered, TimeSpan.Zero);
        set => DateOrdered = value.UtcDateTime;
    }

    #endregion

    #region Sales Channels

    public ObservableCollection<SalesChannelListDto> SalesChannels
    {
        get => _salesChannels;
        set => SetProperty(ref _salesChannels, value);
    }

    #endregion

    #region UI State

    /// <summary>
    /// Indicates whether a save operation is in progress.
    /// </summary>
    public bool IsSaving
    {
        get => _isSaving;
        private set
        {
            if (SetProperty(ref _isSaving, value))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    /// <summary>
    /// Combined loading state (initializing or saving).
    /// </summary>
    public bool IsLoading => IsInitializing || IsSaving;

    /// <summary>
    /// Inverse of IsLoading for binding convenience.
    /// </summary>
    public bool IsNotLoading => !IsLoading;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool CanSave => !IsLoading;

    #endregion

    private async Task LoadOrderAsync(CancellationToken ct)
    {
        if (!_orderId.HasValue) return;

        var order = await _orderService.GetOrderAsync(_orderId.Value, ct);
        if (order != null)
        {
            // Order Information
            OrderIdNumber = order.OrderId;
            RemoteOrderId = order.RemoteOrderId ?? string.Empty;
            SalesChannelId = order.SalesChannelId;
            CustomerId = order.CustomerId;

            // Status
            Status = order.Status;
            PaymentStatus = order.PaymentStatus;

            // Payment Information
            PaymentMethod = order.PaymentMethod ?? string.Empty;
            PaymentProvider = order.PaymentProvider ?? string.Empty;
            PaymentTransactionId = order.PaymentTransactionId ?? string.Empty;

            // Notes - OrderDetailDto only has Note, map to CustomerNote
            CustomerNote = order.Note ?? string.Empty;
            // InternalNote is not available in OrderDetailDto, leave at default

            // Financial Information
            Subtotal = order.Subtotal;
            ShippingCost = order.ShippingCost;
            TotalTax = order.TotalTax;
            Total = order.Total;

            // Delivery Address
            DeliveryAddressFirstName = order.DeliveryAddressFirstName ?? string.Empty;
            DeliveryAddressLastName = order.DeliveryAddressLastName ?? string.Empty;
            DeliveryAddressCompanyName = order.DeliveryAddressCompanyName ?? string.Empty;
            DeliveryAddressPhone = order.DeliveryAddressPhone ?? string.Empty;
            DeliveryAddressStreet = order.DeliveryAddressStreet ?? string.Empty;
            DeliveryAddressCity = order.DeliveryAddressCity ?? string.Empty;
            DeliveryAddressZip = order.DeliveryAddressZip ?? string.Empty;
            DeliveryAddressCountry = order.DeliveryAddressCountry ?? string.Empty;

            // Invoice Address
            InvoiceAddressFirstName = order.InvoiceAddressFirstName ?? string.Empty;
            InvoiceAddressLastName = order.InvoiceAddressLastName ?? string.Empty;
            InvoiceAddressCompanyName = order.InvoiceAddressCompanyName ?? string.Empty;
            InvoiceAddressPhone = order.InvoiceAddressPhone ?? string.Empty;
            InvoiceAddressStreet = order.InvoiceAddressStreet ?? string.Empty;
            InvoiceAddressCity = order.InvoiceAddressCity ?? string.Empty;
            InvoiceAddressZip = order.InvoiceAddressZip ?? string.Empty;
            InvoiceAddressCountry = order.InvoiceAddressCountry ?? string.Empty;

            // Notification Flags - not available in OrderDetailDto, leave at default values

            // Date - OrderDetailDto.DateOrdered is DateTime, not DateTimeOffset
            DateOrdered = order.DateOrdered;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new OrderInputDto
            {
                OrderId = OrderIdNumber,
                RemoteOrderId = RemoteOrderId,
                SalesChannelId = SalesChannelId,
                CustomerId = CustomerId,
                Status = Status,
                PaymentStatus = PaymentStatus,
                PaymentMethod = PaymentMethod,
                PaymentProvider = PaymentProvider,
                PaymentTransactionId = PaymentTransactionId,
                CustomerNote = CustomerNote,
                InternalNote = InternalNote,
                Subtotal = Subtotal,
                ShippingCost = ShippingCost,
                TotalTax = TotalTax,
                Total = Total,
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
                OrderConfirmationSent = OrderConfirmationSent,
                InvoiceSent = InvoiceSent,
                ShippingInformationSent = ShippingInformationSent,
                DateOrdered = DateOrdered
            };

            if (_orderId.HasValue)
            {
                input.Id = _orderId.Value;
                await _orderService.UpdateOrderAsync(_orderId.Value, input, ct);
            }
            else
            {
                await _orderService.CreateOrderAsync(input, ct);
            }

            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["OrderEditPage.Error.SaveFailed"], ex.Message);
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task CancelAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Copies the delivery address to the invoice address.
    /// </summary>
    public void CopyDeliveryToInvoiceAddress()
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

    /// <summary>
    /// Copies the invoice address to the delivery address.
    /// </summary>
    public void CopyInvoiceToDeliveryAddress()
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

    /// <inheritdoc />
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}

/// <summary>
/// Represents an order status option for the ComboBox.
/// </summary>
public record OrderStatusOption(OrderStatus Value, string ResourceKey);

/// <summary>
/// Represents a payment status option for the ComboBox.
/// </summary>
public record PaymentStatusOption(PaymentStatus Value, string ResourceKey);
