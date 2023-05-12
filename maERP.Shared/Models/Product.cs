using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace maERP.Shared.Models;

[Index(nameof(Sku), IsUnique = true)]
public class Product : ABaseModel
{
    [Required, StringLength(255), Display(Name = "SKU")]
    public virtual string Sku { get; set; } = string.Empty;

    [Required, StringLength(255), Display(Name = "Produktname")]
    public virtual string Name { get; set; } = string.Empty;

    [StringLength(32), Display(Name = "EAN")]
    public virtual string Ean { get; set; } = string.Empty;

    [StringLength(32), Display(Name = "ASIN")]
    public virtual string Asin { get; set; } = string.Empty;

    [StringLength(64000), Display(Name = "Produktbeschreibung")]
    public virtual string Description { get; set; } = string.Empty;

    [Required, Display(Name = "Preis")]
    public virtual decimal Price { get; set; }

    [Display(Name = "UVP")]
    public virtual decimal Msrp { get; set; }

    [Required, Display(Name = "Steuerklasse")]
    public TaxClass? TaxClass { get; set; }

    public ICollection<ProductSalesChannel>? ProductSalesChannel { get; set; } = new List<ProductSalesChannel>();

    public ICollection<ProductStock> ProductStock { get; set; } = new List<ProductStock>();
}