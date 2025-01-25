namespace maERP.SalesChannels.Models.Shopware5;

public class Payment
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string template { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string table { get; set; } = string.Empty;
    public bool hide { get; set; }
    public string additionalDescription { get; set; } = string.Empty;
    public int debitPercent { get; set; }
    public int surcharge { get; set; }
    public string surchargeString { get; set; } = string.Empty;
    public int position { get; set; }
    public bool active { get; set; }
    public bool esdActive { get; set; }
    public string embedIFrame { get; set; } = string.Empty;
    public int hideProspect { get; set; }
    public string action { get; set; } = string.Empty;
    // public object pluginId { get; set; } = new object();
    // public object source { get; set; } = new object(); 
}