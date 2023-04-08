#nullable disable

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
    public string SKU { get; set; }

    [Required]
    [StringLength(255)]
    [Column("name")]
    public string Name { get; set; }

    [StringLength(32)]
    [Column("ean")]
    public string EAN { get; set; }

    [StringLength(32)]
    [Column("asin")]
    public string ASIN { get; set; }

    [StringLength(64000)]
    [Column("description")]
    public string Description { get; set; }

    [Required]
    [Column("price")]
    public decimal Price { get; set; }

    [Column("msrp")]
    public decimal Msrp { get; set; }

    [Required]
    [Column("tax_class_id")]
    public TaxClass TaxClass { get; set; }

    public ICollection<ProductSalesChannel> ProductSalesChannel { get; set; }

    public ICollection<ProductStock> ProductStock { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}