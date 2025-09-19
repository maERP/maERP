using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class OrderItemSerialNumber : BaseEntity, IBaseEntity
{
    public Guid OrderItemId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
}