namespace maERP.SalesChannels.Models.Shopware5;

public class Languagesubshop
{
    public int id { get; set; }
    public object mainId { get; set; } = new object();
    public int categoryId { get; set; }
    public string name { get; set; } = string.Empty;
    public object title { get; set; } = new object();
    public int position { get; set; }
    public object host { get; set; } = new object();
    public string basePath { get; set; } = string.Empty;
    public object baseUrl { get; set; } = new object();
    public string hosts { get; set; } = string.Empty;
    public bool secure { get; set; }
    public bool alwaysSecure { get; set; }
    public object secureHost { get; set; } = new object();
    public object secureBasePath { get; set; } = new object();
    public bool _default { get; set; }
    public bool active { get; set; }
    public bool customerScope { get; set; }
    public Locale locale { get; set; } = new Locale();
}
