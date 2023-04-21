using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace maERP.Shared.Models;

[Index(nameof(Sku), IsUnique = true)]
public class Product : BaseModel
{
    [Required]
    [StringLength(255)]
    public virtual string Sku { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public virtual string Name { get; set; } = string.Empty;

    [StringLength(32)]
    public virtual string Ean { get; set; } = string.Empty;

    [StringLength(32)]
    public virtual string Asin { get; set; } = string.Empty;

    [StringLength(64000)]
    public virtual string Description { get; set; } = string.Empty;

    [Required]
    public virtual decimal Price { get; set; }

    public virtual decimal Msrp { get; set; }

    [Required]
    public virtual TaxClass? TaxClass { get; set; }

    public virtual ICollection<ProductSalesChannel>? ProductSalesChannel { get; set; }

    public virtual ICollection<ProductStock>? ProductStock { get; set; }
}