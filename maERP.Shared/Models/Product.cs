using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Product : BaseModel
{
    [StringLength(255)]
    public virtual string SKU { get; set; } = string.Empty;

    [StringLength(255)]
    public virtual string Name { get; set; } = string.Empty;

    [StringLength(32)]
    public virtual string EAN { get; set; } = string.Empty;

    [StringLength(32)]
    public virtual string ASIN { get; set; } = string.Empty;

    [StringLength(64000)]
    public virtual string Description { get; set; } = string.Empty;

    public virtual decimal Price { get; set; }

    public virtual decimal Msrp { get; set; }

    public virtual TaxClass TaxClass { get; set; }

    public virtual ICollection<ProductSalesChannel> ProductSalesChannel { get; set; }

    public virtual ICollection<ProductStock> ProductStock { get; set; }
}