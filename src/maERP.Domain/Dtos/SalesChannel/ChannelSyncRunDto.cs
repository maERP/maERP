using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.SalesChannel;

/// <summary>
/// Wire-format projection of <c>ChannelSyncRun</c> for the Client UI's history view.
/// Mirrors the entity 1:1 — no field hiding, no aggregation. Add new fields here when the entity grows.
/// </summary>
public class ChannelSyncRunDto
{
    public Guid Id { get; set; }
    public Guid SalesChannelId { get; set; }
    public ChannelSyncOperation Operation { get; set; }
    public ChannelSyncTriggerSource TriggerSource { get; set; }
    public ChannelSyncRunStatus Status { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public int ItemsProcessed { get; set; }
    public int ItemsFailed { get; set; }
    public string? ErrorSummary { get; set; }
    public Guid CorrelationId { get; set; }
}

/// <summary>Wire-format projection of <c>ChannelExportOutbox</c> for the Client UI's dead-letter view.</summary>
public class ChannelExportOutboxDto
{
    public Guid Id { get; set; }
    public Guid SalesChannelId { get; set; }
    public ChannelSyncOperation Operation { get; set; }
    public ChannelOutboxAggregateType AggregateType { get; set; }
    public Guid AggregateId { get; set; }
    public string IdempotencyKey { get; set; } = string.Empty;
    public int AttemptCount { get; set; }
    public DateTime NextAttemptAt { get; set; }
    public ChannelOutboxStatus Status { get; set; }
    public string? LastError { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

/// <summary>Result of a manual sync trigger or test-connection request.</summary>
public class SalesChannelSyncResultDto
{
    public Guid? RunId { get; set; }
    public ChannelSyncRunStatus? Status { get; set; }
    public int ItemsProcessed { get; set; }
    public int ItemsFailed { get; set; }
    public string? ErrorSummary { get; set; }
    public bool? Success { get; set; }
    public string? Message { get; set; }
}
