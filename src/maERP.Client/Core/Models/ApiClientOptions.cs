namespace maERP.Client.Core.Models;

/// <summary>
/// Configuration options for the API client
/// </summary>
public record ApiClientOptions
{
    /// <summary>
    /// Base URL of the maERP.Server API (e.g., "https://api.maerp.com" or "http://localhost:5000")
    /// </summary>
    public string BaseUrl { get; init; } = string.Empty;

    /// <summary>
    /// API version to use (default: 1.0)
    /// </summary>
    public string ApiVersion { get; init; } = "1.0";

    /// <summary>
    /// HTTP request timeout in seconds (default: 30)
    /// </summary>
    public int TimeoutSeconds { get; init; } = 30;

    /// <summary>
    /// Whether to use native HTTP handler (better for mobile platforms)
    /// </summary>
    public bool UseNativeHandler { get; init; } = true;

    /// <summary>
    /// Maximum number of retry attempts for failed requests (default: 3)
    /// </summary>
    public int MaxRetryAttempts { get; init; } = 3;

    /// <summary>
    /// Enable detailed logging for HTTP requests (Debug builds only)
    /// </summary>
    public bool EnableDetailedLogging { get; init; } = false;
}
