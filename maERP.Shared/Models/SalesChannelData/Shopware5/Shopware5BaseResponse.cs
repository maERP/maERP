#nullable disable

using maERP;

namespace maERP.Shared.Models.SalesChannelData.Shopware5;

public class Shopware5Response<T> where T : class
{
    public List<T> data { get; set; }
    public int total { get; set; }
    public bool success { get; set; }
}
