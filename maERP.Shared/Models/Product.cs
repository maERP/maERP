using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Product
{
    [Required]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    [Column("sku")]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(32)]
    [Column("ean")]
    public string EAN { get; set; } = string.Empty;

    [StringLength(32)]
    [Column("asin")]
    public string ASIN { get; set; } = string.Empty;

    [StringLength(64000)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("price")]
    public decimal Price { get; set; }

    [Column("msrp")]
    public decimal Msrp { get; set; }

    [Required]
    [Column("tax_class_id")]
    public virtual TaxClass? TaxClass { get; set; }

    public virtual ICollection<ProductSalesChannel>? ProductSalesChannel { get; set; }

    public virtual ICollection<ProductStock>? ProductStock { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}