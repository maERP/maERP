using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Product
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(32)]
    public string EAN { get; set; } = string.Empty;

    [StringLength(32)]
    public string ASIN { get; set; } = string.Empty;

    [StringLength(64000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public decimal Msrp { get; set; }

    [Required]
    public virtual TaxClass? TaxClass { get; set; }

    public virtual ICollection<ProductSalesChannel>? ProductSalesChannel { get; set; }

    public virtual ICollection<ProductStock>? ProductStock { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}