using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using maERP.Shared.Dtos.ProductSalesChannel;
using maERP.Shared.Dtos.TaxClass;

namespace maERP.Shared.Dtos.Product;

public class ProductCreateDto
{
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
    public virtual TaxClassReferenceDto TaxClass { get; set; }

    public List<ProductSalesChannelReferenceDto> ProductSalesChannel { get; set; }
}