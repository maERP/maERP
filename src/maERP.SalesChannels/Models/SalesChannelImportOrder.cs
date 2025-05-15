using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.SalesChannels.Models;

public class SalesChannelImportOrder
{
    public int SalesChannelId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;
    [Required]
    public string RemoteCustomerId { get; set; } = string.Empty;

    [Required]
    public OrderStatus Status { get; set; }
    
    // Kundeninformationen
    public SalesChannelImportCustomer? Customer { get; set; }
    public SalesChannelImportCustomerAddress BillingAddress { get; set; } = new();
    public SalesChannelImportCustomerAddress ShippingAddress { get; set; } = new();
    
    // Bestellpositionen (durch OrderItems ersetzen, da es im Repository verwendet wird)
    public ICollection<SalesChannelImportOrderItem> OrderItems { get; set; } = new List<SalesChannelImportOrderItem>();

    // Zahlungsinformationen
    public PaymentStatus PaymentStatus { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentProvider { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;

    // Preisinformationen
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalTax { get; set; }
    public decimal Total { get; set; }

    public string CustomerNote { get; set; } = string.Empty;

    // Legacy-Adressfelder (für Kompatibilität)
    public string DeliveryAddressFirstName { get; set; } = string.Empty;
    public string DeliveryAddressLastName { get; set; } = string.Empty;
    public string DeliveryAddressCompanyName { get; set; } = string.Empty;
    public string DeliveryAddressPhone { get; set; } = string.Empty;
    public string DeliveryAddressStreet { get; set; } = string.Empty;
    public string DeliveryAddressCity { get; set; } = string.Empty;
    public string DeliverAddressZip { get; set; } = string.Empty;
    public string DeliveryAddressCountry { get; set; } = string.Empty;

    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public string InvoiceAddressCompanyName { get; set; } = string.Empty;
    public string InvoiceAddressPhone { get; set; } = string.Empty;
    public string InvoiceAddressStreet { get; set; } = string.Empty;
    public string InvoiceAddressCity { get; set; } = string.Empty;
    public string InvoiceAddressZip { get; set; } = string.Empty;
    public string InvoiceAddressCountry { get; set; } = string.Empty;

    public DateTime DateOrdered { get; set; }
}