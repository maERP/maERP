namespace maERP.SalesChannels.Models.Shopware5;

public class BaseListResponse<T> where T : class
{
    public List<T>? data { get; set; }
    public int total { get; set; }
    public bool success { get; set; }
}