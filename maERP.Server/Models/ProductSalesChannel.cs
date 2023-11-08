using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace maERP.Server.Models;

public class ProductSalesChannel : BaseModel
{
    public SalesChannel SalesChannel { get; set; } = new();

    public int ProductId { get; set; } = new();

    public Product Product { get; set; } = new();

    [Required, Display(Name = "externe Product ID")]
    public int RemoteProductId { get; set; }

    [Display(Name = "Preis")]
    public decimal Price { get; set; }
}