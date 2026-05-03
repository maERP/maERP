namespace maERP.SalesChannels.Abstractions;

public sealed record ConnectionTestResult(bool Success, string? Message = null);

/// <summary>
/// Outcome of a single connector method invocation. <c>ItemsProcessed</c> + <c>ItemsFailed</c>
/// flow into the <c>ChannelSyncRun</c> audit row. Connectors that pull pages should sum across
/// pages so the audit reflects the whole run.
/// </summary>
public sealed record SyncResult(
    int ItemsProcessed,
    int ItemsFailed,
    string? ErrorSummary = null)
{
    public static SyncResult Empty => new(0, 0);
    public static SyncResult Failed(string message) => new(0, 0, message);

    public bool IsPartialFailure => ItemsFailed > 0 && ItemsProcessed > 0;
    public bool IsSuccess => ItemsFailed == 0 && ErrorSummary is null;
}

public sealed record ExportResult(
    bool Success,
    string? RemoteId = null,
    string? RemoteListingId = null,
    string? ErrorMessage = null)
{
    public static ExportResult Ok(string? remoteId = null, string? remoteListingId = null)
        => new(true, remoteId, remoteListingId);
    public static ExportResult Fail(string message) => new(false, ErrorMessage: message);
}
