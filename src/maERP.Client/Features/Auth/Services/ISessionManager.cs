namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Manages session lifecycle: periodic token refresh, inactivity timeout, and app lifecycle.
/// </summary>
public interface ISessionManager
{
    /// <summary>
    /// Starts the session timer (called after successful authentication).
    /// </summary>
    Task StartAsync();

    /// <summary>
    /// Stops the session timer (called on logout).
    /// </summary>
    Task StopAsync();

    /// <summary>
    /// Records user activity to reset the inactivity timer.
    /// </summary>
    void RecordUserActivity();

    /// <summary>
    /// Called when the app resumes from suspended/deactivated state.
    /// Checks elapsed time and refreshes token or logs out if needed.
    /// </summary>
    Task OnAppResumedAsync();

    /// <summary>
    /// Called when the app is suspended/deactivated.
    /// Records the suspension timestamp.
    /// </summary>
    void OnAppSuspended();

    /// <summary>
    /// Whether the session manager is actively running.
    /// </summary>
    bool IsActive { get; }
}
