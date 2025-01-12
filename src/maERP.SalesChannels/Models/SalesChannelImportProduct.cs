namespace maERP.SalesChannels.Models;

public class SalesChannelImportProduct
{
    // public int? Id { get; set; }
    public int RemoteProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double TaxRate { get; set; }
    public decimal Price { get; set; }
    public double Stock { get; set; }
}