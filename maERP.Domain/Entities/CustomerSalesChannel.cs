using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class CustomerSalesChannel : BaseEntity, IBaseEntity
{
    public required int CustomerId { get; set; }
    public required int SalesChannelId { get; set; }
    public required string RemoteCustomerId { get; set; } = string.Empty;
}