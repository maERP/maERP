using System.ComponentModel.DataAnnotations;
using maERP.Domain.Dtos.Manufacturer;

namespace maERP.Domain.Dtos.Product;

public class ProductDetailDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    public string? NameOptimized { get; set; }

    [StringLength(32)]
    public string? Ean { get; set; }

    [StringLength(32)]
    public string? Asin { get; set; }

    [StringLength(64000)]
    public string? Description { get; set; }

    [StringLength(64000)]
    public string? DescriptionOptimized { get; set; }

    public bool UseOptimized { get; set; }

    [Required]
    public decimal Price { get; set; }

    public decimal Msrp { get; set; }

    public decimal Weight { get; set; }

    public decimal Width { get; set; }

    public decimal Height { get; set; }

    public decimal Depth { get; set; }

    public int TaxClassId { get; set; } = new();

    public ManufacturerDetailDto? Manufacturer { get; set; }

    public List<int> ProductSalesChannel { get; set; } = new();

    public List<int> ProductStocks { get; set; } = new();
}