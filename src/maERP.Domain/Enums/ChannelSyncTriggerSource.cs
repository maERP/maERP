namespace maERP.Domain.Enums;

/// <summary>
/// Why this sync run was started. Drives logging, metrics, and (later) per-trigger backoff policies.
/// </summary>
public enum ChannelSyncTriggerSource
{
    Scheduler = 0,
    Manual = 1,
    Event = 2,
}
