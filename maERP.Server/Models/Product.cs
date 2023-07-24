using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Models;

[Index(nameof(Sku), IsUnique = true)]
public class Product : ABaseModel
{
    public virtual string Sku { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Ean { get; set; } = string.Empty;

    public virtual string Asin { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual decimal Price { get; set; }
    public virtual decimal Msrp { get; set; }

    public int TaxClassId { get; set; }
    public TaxClass? TaxClass { get; set; }

    public ICollection<ProductSalesChannel>? ProductSalesChannel { get; set; } = new List<ProductSalesChannel>();

    public ICollection<ProductStock> ProductStock { get; set; } = new List<ProductStock>();
}