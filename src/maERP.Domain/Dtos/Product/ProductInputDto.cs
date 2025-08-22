using System.ComponentModel.DataAnnotations;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.Product;

public class ProductInputDto : IProductInputModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    public string NameOptimized { get; set; } = string.Empty;

    [StringLength(32)]
    public string Ean { get; set; } = string.Empty;

    [StringLength(32)]
    public string Asin { get; set; } = string.Empty;

    [StringLength(64000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(64000)]
    public string DescriptionOptimized { get; set; } = string.Empty;

    public bool UseOptimized { get; set; }

    [Required]
    public decimal Price { get; set; }

    public decimal Msrp { get; set; }

    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Depth { get; set; }

    public int TaxClassId { get; set; } = new();

    public int? ManufacturerId { get; set; }

    public List<int> ProductSalesChannel { get; set; } = new();
}