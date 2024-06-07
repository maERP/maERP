using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Features.Product.Commands.CreateProduct;

public class CreateProductResponse
{
    [Required]
    [StringLength(255)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(32)]
    public string Ean { get; set; } = string.Empty;

    [StringLength(32)]
    public string Asin { get; set; } = string.Empty;

    [StringLength(64000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public decimal Msrp { get; set; }

    public int TaxClassId { get; set; } = new();

    public List<int> ProductSalesChannel { get; set; } = new();
}