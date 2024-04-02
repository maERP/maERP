using maERP.Domain.Common;

namespace maERP.Domain;

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public List<SalesChannel>? SalesChannels { get; set; }
}