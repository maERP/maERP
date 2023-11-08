using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using maERP.Shared.Dtos.ProductSalesChannel;
using maERP.Shared.Dtos.TaxClass;

namespace maERP.Shared.Dtos.Product;

public class ProductCreateDto
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

    public virtual BaseDto TaxClass { get; set; } = new();

    public List<BaseDto> ProductSalesChannel { get; set; } = new();
}