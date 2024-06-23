using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class OrderItemSerialNumber : BaseEntity, IBaseEntity
{ 
    public int OrderItemId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
}