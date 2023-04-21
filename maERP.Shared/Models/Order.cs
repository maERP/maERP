using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Order : BaseModel
{
    [Required]
    public virtual int CustomerId { get; set; }

    [Required]
    public virtual OrderStatus Status { get; set; }
}