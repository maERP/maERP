using System.ComponentModel.DataAnnotations;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.Invoice;

public class InvoiceInputDto : IInvoiceInputModel
{
    public Guid Id { get; set; }

    [Required]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    public Guid? OrderId { get; set; }

    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalTax { get; set; }
    public decimal Total { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    // Invoice Address
    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public string InvoiceAddressCompanyName { get; set; } = string.Empty;
    public string InvoiceAddressPhone { get; set; } = string.Empty;
    public string InvoiceAddressStreet { get; set; } = string.Empty;
    public string InvoiceAddressCity { get; set; } = string.Empty;
    public string InvoiceAddressZip { get; set; } = string.Empty;
    public string InvoiceAddressCountry { get; set; } = string.Empty;

    // Delivery Address
    public string DeliveryAddressFirstName { get; set; } = string.Empty;
    public string DeliveryAddressLastName { get; set; } = string.Empty;
    public string DeliveryAddressCompanyName { get; set; } = string.Empty;
    public string DeliveryAddressPhone { get; set; } = string.Empty;
    public string DeliveryAddressStreet { get; set; } = string.Empty;
    public string DeliveryAddressCity { get; set; } = string.Empty;
    public string DeliveryAddressZip { get; set; } = string.Empty;
    public string DeliveryAddressCountry { get; set; } = string.Empty;
}