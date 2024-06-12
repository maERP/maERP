using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Warehouse : BaseEntity, IBaseEntity
{
    public string Name { get; set; } = string.Empty;

    public List<SalesChannel>? SalesChannels { get; set; }
}