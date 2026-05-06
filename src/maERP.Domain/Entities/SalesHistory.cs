using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class SalesHistory : BaseEntity, IBaseEntity
{
    public Guid UserId { get; set; }
    public Guid SalesId { get; set; }
    public SalesStatus? SalesStatusOld { get; set; }
    public SalesStatus? SalesStatusNew { get; set; }
    public PaymentStatus? PaymentStatusOld { get; set; }
    public PaymentStatus? PaymentStatusNew { get; set; }
    public string? ShippingStatusOld { get; set; }
    public string? ShippingStatusNew { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsSystemGenerated { get; set; }
}