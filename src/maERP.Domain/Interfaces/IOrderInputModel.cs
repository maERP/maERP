using maERP.Domain.Enums;

namespace maERP.Domain.Interfaces;

public interface IOrderInputModel
{
    Guid SalesChannelId { get; }
    string RemoteOrderId { get; }
    Guid CustomerId { get; }
    OrderStatus Status { get; }
    decimal Subtotal { get; }
    decimal ShippingCost { get; }
    decimal TotalTax { get; }
    decimal Total { get; }
    PaymentStatus PaymentStatus { get; }
    string PaymentMethod { get; }
    string PaymentProvider { get; }
    string PaymentTransactionId { get; }
    string CustomerNote { get; }
    string InternalNote { get; }
    string DeliveryAddressFirstName { get; }
    string DeliveryAddressLastName { get; }
    string DeliveryAddressCompanyName { get; }
    string DeliveryAddressPhone { get; }
    string DeliveryAddressStreet { get; }
    string DeliveryAddressCity { get; }
    string DeliverAddressZip { get; }
    string DeliveryAddressCountry { get; }
    string InvoiceAddressFirstName { get; }
    string InvoiceAddressLastName { get; }
    string InvoiceAddressCompanyName { get; }
    string InvoiceAddressPhone { get; }
    string InvoiceAddressStreet { get; }
    string InvoiceAddressCity { get; }
    string InvoiceAddressZip { get; }
    string InvoiceAddressCountry { get; }
    DateTime DateOrdered { get; }
}
