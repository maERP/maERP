using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class OrderHistory : BaseEntity, IBaseEntity
{
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
                     
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public OrderStatus OldStatus { get; set; }
    
    [Required]
    public OrderStatus NewStatus { get; set; }
    
    public string FieldName { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    
    public string Comment { get; set; } = string.Empty;
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
} 