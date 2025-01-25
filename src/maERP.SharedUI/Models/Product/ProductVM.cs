namespace maERP.SharedUI.Models.Product;

public class ProductVm
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }
}