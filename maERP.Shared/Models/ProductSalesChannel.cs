using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace maERP.Shared.Models;

public class ProductSalesChannel : BaseModel
{
    public virtual SalesChannel SalesChannel { get; set; } = new();

    public virtual Product Product { get; set; } = new();

    [Required, Display(Name = "externe Product ID")]
    public virtual int RemoteProductId { get; set; }

    [Display(Name = "Preis")]
    public virtual decimal Price { get; set; }
}