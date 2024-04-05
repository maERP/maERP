using maERP.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace maERP.Application.Dtos.Order;

public class OrderListDto
{
    public int Id { get; set; }

    [Required, Display(Name = "Kundennummer"), DisplayFormat(NullDisplayText = "0")]
    public int CustomerId { get; set; }

    [Required, Display(Name = "Bestellstatus")]
    public string Status { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }
}