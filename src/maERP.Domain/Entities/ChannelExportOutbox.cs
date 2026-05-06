using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

/// <summary>
/// Reliable-export queue. Domain notifications + the SaveChanges interceptor enqueue rows here;
/// the orchestrator drains them and dispatches to the channel connector. Exponential backoff on
/// failure; rows that exceed the retry budget land in <see cref="ChannelOutboxStatus.DeadLetter"/>.
/// </summary>
public class ChannelExportOutbox : BaseEntity, IBaseEntity
{
    public Guid SalesChannelId { get; set; }
    public SalesChannel? SalesChannel { get; set; }

    public ChannelSyncOperation Operation { get; set; }
    public ChannelOutboxAggregateType AggregateType { get; set; }

    /// <summary>Id of the aggregate being exported (Product.Id, Sales.Id, ProductSalesChannel.Id, ...).</summary>
    public Guid AggregateId { get; set; }

    /// <summary>Serialized payload — channel-agnostic; the connector translates it to its API DTO.</summary>
    public string PayloadJson { get; set; } = string.Empty;

    /// <summary>
    /// SHA-256 of <c>"{Op}:{AggregateType}:{AggregateId}:{Hash(payload)}"</c>. Unique per
    /// (SalesChannelId, IdempotencyKey) — prevents duplicate enqueue when the same change
    /// fires multiple notifications.
    /// </summary>
    public string IdempotencyKey { get; set; } = string.Empty;

    public int AttemptCount { get; set; }
    public DateTime NextAttemptAt { get; set; }

    public ChannelOutboxStatus Status { get; set; }

    /// <summary>Truncated last error message — full stack in logs.</summary>
    public string? LastError { get; set; }

    public DateTime? CompletedAt { get; set; }
}
