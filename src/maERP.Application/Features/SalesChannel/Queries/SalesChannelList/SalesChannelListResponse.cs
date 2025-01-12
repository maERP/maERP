namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelList;

public class SalesChannelListResponse
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}