namespace maERP.SalesChannels.Models.Shopware5;

public class BaseResponse<T> where T : class
{
    public T? data { get; set; }
    public bool success { get; set; }
}