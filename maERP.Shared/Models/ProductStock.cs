using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductStock : BaseModel
{
    public virtual Product Product { get; set; }

    public virtual Warehouse Warehouse { get; set; }

    public int Quantity { get; set; }
}