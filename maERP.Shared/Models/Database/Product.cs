using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace maERP.Shared.Models.Database;

[Index(nameof(Sku), IsUnique = true)]
public class Product : BaseModel
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;

    public string Asin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }

    public int TaxClassId { get; set; }
    public TaxClass? TaxClass { get; set; }

    public ICollection<ProductSalesChannel>? ProductSalesChannel { get; set; } = new List<ProductSalesChannel>();

    public ICollection<ProductStock> ProductStock { get; set; } = new List<ProductStock>();
}