using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Warehouse : BaseEntity, IBaseEntity
{
    public string Name { get; set; } = string.Empty;

    public List<SalesChannel>? SalesChannels { get; set; }
}