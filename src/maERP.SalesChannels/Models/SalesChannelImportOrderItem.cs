namespace maERP.SalesChannels.Models;

public class SalesChannelImportOrderItem
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public decimal Price { get; set; }
    public double TaxRate { get; set; }
}