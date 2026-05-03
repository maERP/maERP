using maERP.Application.Mediator;
using maERP.Domain.Enums;

namespace maERP.Application.Notifications;

/// <summary>
/// Raised after a SalesChannel successfully completes its OAuth Authorization-Code flow and a
/// fresh refresh token has been persisted. Used for audit logging (Serilog → Grafana) and for
/// telling the orchestrator to retry any DeadLetter outbox rows that previously failed because
/// the channel was unauthenticated.
/// </summary>
public sealed record SalesChannelOAuthConnectedNotification(
    Guid SalesChannelId,
    Guid TenantId,
    SalesChannelType Provider,
    string? Marketplace,
    DateTime ConnectedAt) : INotification;
