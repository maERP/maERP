using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class Order : BaseEntity, IBaseEntity
{
    public Guid SalesChannelId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;

    [Required]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    [Required]
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalTax { get; set; }
    public decimal Total { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentProvider { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;

    public string CustomerNote { get; set; } = string.Empty;
    public string InternalNote { get; set; } = string.Empty;

    public string DeliveryAddressFirstName { get; set; } = string.Empty;
    public string DeliveryAddressLastName { get; set; } = string.Empty;
    public string DeliveryAddressCompanyName { get; set; } = string.Empty;
    public string DeliveryAddressPhone { get; set; } = string.Empty;
    public string DeliveryAddressStreet { get; set; } = string.Empty;
    public string DeliveryAddressCity { get; set; } = string.Empty;
    public string DeliveryAddressZip { get; set; } = string.Empty;
    public string DeliveryAddressCountry { get; set; } = string.Empty;

    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public string InvoiceAddressCompanyName { get; set; } = string.Empty;
    public string InvoiceAddressPhone { get; set; } = string.Empty;
    public string InvoiceAddressStreet { get; set; } = string.Empty;
    public string InvoiceAddressCity { get; set; } = string.Empty;
    public string InvoiceAddressZip { get; set; } = string.Empty;
    public string InvoiceAddressCountry { get; set; } = string.Empty;

    public bool OrderConfirmationSent { get; set; }
    public bool InvoiceSent { get; set; }
    public bool ShippingInformationSent { get; set; }

    public DateTime DateOrdered { get; set; }

    public ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}