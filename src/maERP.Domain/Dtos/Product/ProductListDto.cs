using maERP.Domain.Dtos.Manufacturer;

namespace maERP.Domain.Dtos.Product;

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Ean { get; set; }
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }
    public ManufacturerListDto? Manufacturer { get; set; }
}