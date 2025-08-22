using maERP.Domain.Enums;

namespace maERP.Domain.Interfaces;

/// <summary>
/// Interface for invoice input models, defining properties required for invoice validation.
/// </summary>
public interface IInvoiceInputModel
{
    string InvoiceNumber { get; set; }
    DateTime InvoiceDate { get; set; }
    int CustomerId { get; set; }
    int? OrderId { get; set; }

    decimal Subtotal { get; set; }
    decimal ShippingCost { get; set; }
    decimal TotalTax { get; set; }
    decimal Total { get; set; }

    PaymentStatus PaymentStatus { get; set; }
    InvoiceStatus InvoiceStatus { get; set; }
    string PaymentMethod { get; set; }
    string PaymentTransactionId { get; set; }

    string Notes { get; set; }

    // Invoice Address
    string InvoiceAddressFirstName { get; set; }
    string InvoiceAddressLastName { get; set; }
    string InvoiceAddressCompanyName { get; set; }
    string InvoiceAddressPhone { get; set; }
    string InvoiceAddressStreet { get; set; }
    string InvoiceAddressCity { get; set; }
    string InvoiceAddressZip { get; set; }
    string InvoiceAddressCountry { get; set; }

    // Delivery Address
    string DeliveryAddressFirstName { get; set; }
    string DeliveryAddressLastName { get; set; }
    string DeliveryAddressCompanyName { get; set; }
    string DeliveryAddressPhone { get; set; }
    string DeliveryAddressStreet { get; set; }
    string DeliveryAddressCity { get; set; }
    string DeliveryAddressZip { get; set; }
    string DeliveryAddressCountry { get; set; }
}
