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
    public virtual OrderStatus Status { get; set; }
}