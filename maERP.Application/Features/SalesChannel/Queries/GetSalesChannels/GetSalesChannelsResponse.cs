namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannels;

public class GetSalesChannelsResponse
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}