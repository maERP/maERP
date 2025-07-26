using maERP.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace maERP.Domain.Entities;

[Index(nameof(Sku), IsUnique = true)]
public class Product : BaseEntity, IBaseEntity
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NameOptimized { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public string Asin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionOptimized { get; set; } = string.Empty; 
    
    public bool UseOptimized { get; set; }
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Depth { get; set; }

    public int TaxClassId { get; set; }
    public Manufacturer? Manufacturer { get; set; }
    public TaxClass? TaxClass { get; set; }

    public ICollection<ProductSalesChannel>? ProductSalesChannels { get; set; } = [];

    public ICollection<ProductStock> ProductStocks { get; set; } = [];
}