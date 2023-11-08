using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Server.Models;

public class Order : BaseModel
{
    public int SalesChannelId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;

    [Required, Display(Name = "Kundennummer"), DisplayFormat(NullDisplayText = "0")]
    public int CustomerId { get; set; }

    [Required, Display(Name = "Bestellstatus")]
    public OrderStatus Status { get; set; }
}