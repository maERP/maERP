namespace maERP.SalesChannels.Models.Shopware5;

public class Detail
{
    public int id { get; set; }
    public int orderId { get; set; }
    public int articleId { get; set; }
    public int taxId { get; set; }
    public int taxRate { get; set; }
    public int statusId { get; set; }
    public string number { get; set; } = string.Empty;
    public string articleNumber { get; set; } = string.Empty;
    public decimal price { get; set; }
    public decimal quantity { get; set; }
    public string articleName { get; set; } = string.Empty;
    public int shipped { get; set; }
    public int shippedGroup { get; set; }
    public string releaseDate { get; set; } = string.Empty;
    public int mode { get; set; }
    public int esdArticle { get; set; }
    public string config { get; set; } = string.Empty;
    public string ean { get; set; } = string.Empty;
    public string unit { get; set; } = string.Empty;
    public string packUnit { get; set; } = string.Empty;
    // public Attribute3 attribute { get; set; } = new Attribute3();
}
