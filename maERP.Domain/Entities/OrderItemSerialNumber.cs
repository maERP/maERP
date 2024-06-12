using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class OrderItemSerialNumber : BaseEntity, IBaseEntity
{ 
    public int OrderItemId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
}