using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

public class OrderDetailResponse
{
    public int SalesChannelId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public string PaymentStatus { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentProvider { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;

    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalTax { get; set; }
    public decimal Total { get; set; }

    public string Note { get; set; } = string.Empty;

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