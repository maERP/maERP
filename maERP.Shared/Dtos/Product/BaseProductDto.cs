#nullable disable

namespace maERP.Shared.Dtos.Product;

public class BaseProductDto
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int TaxClassId { get; set; }
}