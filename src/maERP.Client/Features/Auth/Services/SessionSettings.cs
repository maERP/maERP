namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Configuration settings for session management (keep-alive and inactivity timeout).
/// </summary>
public class SessionSettings
{
    /// <summary>
    /// Interval in minutes between keep-alive token refresh calls while the app is focused.
    /// </summary>
    public int KeepAliveIntervalMinutes { get; set; } = 5;

    /// <summary>
    /// Maximum inactivity duration in minutes before the session is invalidated.
    /// If the app regains focus after this period, the user is logged out.
    /// </summary>
    public int InactivityTimeoutMinutes { get; set; } = 30;

    /// <summary>
    /// Interval in seconds for the periodic timer that checks token expiry and inactivity.
    /// </summary>
    public int TokenRefreshCheckIntervalSeconds { get; set; } = 60;

    /// <summary>
    /// Buffer in minutes before token expiry to trigger a proactive refresh.
    /// </summary>
    public int TokenExpiryBufferMinutes { get; set; } = 2;
}
