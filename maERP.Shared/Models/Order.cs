using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Order : ABaseModel
{
    [Required, Display(Name = "Kundennummer"), DisplayFormat(NullDisplayText = "0")]
    public virtual uint CustomerId { get; set; }

    [Required, Display(Name = "Bestellstatus")]
    public virtual OrderStatus Status { get; set; }
}