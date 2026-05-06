using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Saless.Services;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Saless.Models;

/// <summary>
/// Model for sales edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class SalesEditModel : AsyncInitializableModel
{
    private readonly ISalesService _salesService;
    private readonly ISalesChannelService _salesChannelService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _salesId;

    // Sales Information
    private int _salesIdNumber;
    private string _remoteSalesId = string.Empty;
    private Guid _salesChannelId;
    private int _customerId;

    // Status
    private SalesStatus _status = SalesStatus.Pending;
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
    private bool _salesConfirmationSent;
    private bool _invoiceSent;
    private bool _shippingInformationSent;

    // Date
    private DateTime _dateSalesed = DateTime.UtcNow;

    // Sales Channels
    private ObservableCollection<SalesChannelListDto> _salesChannels = new();

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public SalesEditModel(
        ISalesService salesService,
        ISalesChannelService salesChannelService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<SalesEditModel> logger,
        SalesEditData? data = null)
        : base(logger)
    {
        _salesService = salesService;
        _salesChannelService = salesChannelService;
        _navigator = navigator;
        _localizer = localizer;
        _salesId = data?.salesId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        await LoadSalesChannelsAsync(ct);

        if (_salesId.HasValue)
        {
            await LoadSalesAsync(ct);
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

    public bool IsEditMode => _salesId.HasValue;

    public string Title => IsEditMode
        ? _localizer["SalesEditPage.TitleEdit"]
        : _localizer["SalesEditPage.TitleNew"];

    /// <summary>
    /// Available sales status options for the ComboBox.
    /// </summary>
    public IReadOnlyList<SalesStatusOption> SalesStatusOptions { get; } = new List<SalesStatusOption>
    {
        new(SalesStatus.Unknown, "SalesStatus.Unknown"),
        new(SalesStatus.Pending, "SalesStatus.Pending"),
        new(SalesStatus.Processing, "SalesStatus.Processing"),
        new(SalesStatus.ReadyForDelivery, "SalesStatus.ReadyForDelivery"),
        new(SalesStatus.PartiallyDelivered, "SalesStatus.PartiallyDelivered"),
        new(SalesStatus.Completed, "SalesStatus.Completed"),
        new(SalesStatus.Cancelled, "SalesStatus.Cancelled"),
        new(SalesStatus.Returned, "SalesStatus.Returned"),
        new(SalesStatus.Refunded, "SalesStatus.Refunded"),
        new(SalesStatus.OnHold, "SalesStatus.OnHold"),
        new(SalesStatus.Failed, "SalesStatus.Failed")
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

    #region Sales Information

    public int SalesIdNumber
    {
        get => _salesIdNumber;
        set => SetProperty(ref _salesIdNumber, value);
    }

    public string RemoteSalesId
    {
        get => _remoteSalesId;
        set => SetProperty(ref _remoteSalesId, value);
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

    public SalesStatus Status
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

    public bool SalesConfirmationSent
    {
        get => _salesConfirmationSent;
        set => SetProperty(ref _salesConfirmationSent, value);
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

    public DateTime DateSalesed
    {
        get => _dateSalesed;
        set => SetProperty(ref _dateSalesed, value);
    }

    public DateTimeOffset DateSalesedOffset
    {
        get => new DateTimeOffset(DateSalesed, TimeSpan.Zero);
        set => DateSalesed = value.UtcDateTime;
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

    private async Task LoadSalesAsync(CancellationToken ct)
    {
        if (!_salesId.HasValue) return;

        var sales = await _salesService.GetSalesAsync(_salesId.Value, ct);
        if (sales != null)
        {
            // Sales Information
            SalesIdNumber = sales.SalesId;
            RemoteSalesId = sales.RemoteSalesId ?? string.Empty;
            SalesChannelId = sales.SalesChannelId;
            CustomerId = sales.CustomerId;

            // Status
            Status = sales.Status;
            PaymentStatus = sales.PaymentStatus;

            // Payment Information
            PaymentMethod = sales.PaymentMethod ?? string.Empty;
            PaymentProvider = sales.PaymentProvider ?? string.Empty;
            PaymentTransactionId = sales.PaymentTransactionId ?? string.Empty;

            // Notes - SalesDetailDto only has Note, map to CustomerNote
            CustomerNote = sales.Note ?? string.Empty;
            // InternalNote is not available in SalesDetailDto, leave at default

            // Financial Information
            Subtotal = sales.Subtotal;
            ShippingCost = sales.ShippingCost;
            TotalTax = sales.TotalTax;
            Total = sales.Total;

            // Delivery Address
            DeliveryAddressFirstName = sales.DeliveryAddressFirstName ?? string.Empty;
            DeliveryAddressLastName = sales.DeliveryAddressLastName ?? string.Empty;
            DeliveryAddressCompanyName = sales.DeliveryAddressCompanyName ?? string.Empty;
            DeliveryAddressPhone = sales.DeliveryAddressPhone ?? string.Empty;
            DeliveryAddressStreet = sales.DeliveryAddressStreet ?? string.Empty;
            DeliveryAddressCity = sales.DeliveryAddressCity ?? string.Empty;
            DeliveryAddressZip = sales.DeliveryAddressZip ?? string.Empty;
            DeliveryAddressCountry = sales.DeliveryAddressCountry ?? string.Empty;

            // Invoice Address
            InvoiceAddressFirstName = sales.InvoiceAddressFirstName ?? string.Empty;
            InvoiceAddressLastName = sales.InvoiceAddressLastName ?? string.Empty;
            InvoiceAddressCompanyName = sales.InvoiceAddressCompanyName ?? string.Empty;
            InvoiceAddressPhone = sales.InvoiceAddressPhone ?? string.Empty;
            InvoiceAddressStreet = sales.InvoiceAddressStreet ?? string.Empty;
            InvoiceAddressCity = sales.InvoiceAddressCity ?? string.Empty;
            InvoiceAddressZip = sales.InvoiceAddressZip ?? string.Empty;
            InvoiceAddressCountry = sales.InvoiceAddressCountry ?? string.Empty;

            // Notification Flags - not available in SalesDetailDto, leave at default values

            // Date - SalesDetailDto.DateSalesed is DateTime, not DateTimeOffset
            DateSalesed = sales.DateSalesed;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new SalesInputDto
            {
                SalesId = SalesIdNumber,
                RemoteSalesId = RemoteSalesId,
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
                SalesConfirmationSent = SalesConfirmationSent,
                InvoiceSent = InvoiceSent,
                ShippingInformationSent = ShippingInformationSent,
                DateSalesed = DateSalesed
            };

            if (_salesId.HasValue)
            {
                input.Id = _salesId.Value;
                await _salesService.UpdateSalesAsync(_salesId.Value, input, ct);
            }
            else
            {
                await _salesService.CreateSalesAsync(input, ct);
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
            ErrorMessage = string.Format(_localizer["SalesEditPage.Error.SaveFailed"], ex.Message);
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
/// Represents an sales status option for the ComboBox.
/// </summary>
public record SalesStatusOption(SalesStatus Value, string ResourceKey);

/// <summary>
/// Represents a payment status option for the ComboBox.
/// </summary>
public record PaymentStatusOption(PaymentStatus Value, string ResourceKey);
