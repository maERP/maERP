using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using maERP.Domain.Common;

namespace maERP.Domain;

public class Order : BaseEntity
{
    public int SalesChannelId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;

    [Required, Display(Name = "Kundennummer"), DisplayFormat(NullDisplayText = "0")]
    public int CustomerId { get; set; }

    [Required, Display(Name = "Bestellstatus")]
    public OrderStatus Status { get; set; }
}