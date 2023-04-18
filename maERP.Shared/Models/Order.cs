using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Order : BaseModel
{
    public virtual int CustomerId { get; set; }

    public virtual OrderStatus Status { get; set; }
}