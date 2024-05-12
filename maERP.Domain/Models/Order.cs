using System.ComponentModel.DataAnnotations;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Order : BaseEntity
{
    public int SalesChannelId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;

    [Required, Display(Name = "Kundennummer"), DisplayFormat(NullDisplayText = "0")]
    public int CustomerId { get; set; }

    [Required, Display(Name = "Bestellstatus")]
    public OrderStatus Status { get; set; }
    public OrderItem? OrderItems { get; set; }

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