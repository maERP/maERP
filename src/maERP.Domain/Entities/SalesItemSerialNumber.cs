using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class SalesItemSerialNumber : BaseEntity, IBaseEntity
{
    public Guid SalesItemId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
}