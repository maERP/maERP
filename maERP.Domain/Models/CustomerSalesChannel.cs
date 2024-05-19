using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class CustomerSalesChannel : BaseEntity
{
    public required int CustomerId { get; set; }
    public required int SalesChannelId { get; set; }
    public required string RemoteCustomerId { get; set; } = string.Empty;
}