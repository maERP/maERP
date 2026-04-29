using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
#if __WASM__
using System.Runtime.InteropServices.JavaScript;
#endif

namespace maERP.Client.Core.Configuration;

/// <summary>
/// Process-wide runtime configuration for the client. Populated once at
/// startup from <c>/config.json</c> (WASM only — written by the nginx
/// container's docker-entrypoint script). On other platforms the values
/// stay at their defaults.
/// </summary>
public static class RuntimeConfig
{
    /// <summary>
    /// When set, the WASM client locks the API to this URL and hides the
    /// server-URL field on the login screen. <c>null</c> or empty = no
    /// restriction, user enters the URL manually.
    /// </summary>
    public static string? RestrictServerUrl { get; private set; }

    /// <summary>
    /// Whether the server URL is locked to <see cref="RestrictServerUrl"/>.
    /// </summary>
    public static bool IsServerUrlRestricted => !string.IsNullOrWhiteSpace(RestrictServerUrl);

    /// <summary>
    /// Best-effort fetch of <c>/config.json</c>. Always succeeds; on failure
    /// (file missing, malformed, non-WASM platform) the config stays empty.
    /// </summary>
    public static async Task LoadAsync(CancellationToken ct = default)
    {
#if __WASM__
        try
        {
            var origin = GetWindowOrigin();
            if (string.IsNullOrEmpty(origin))
            {
                return;
            }

            using var http = new HttpClient { BaseAddress = new Uri(origin) };

            // Cache-bust so updates after a redeploy are picked up immediately.
            var json = await http.GetStringAsync($"config.json?_={DateTime.UtcNow.Ticks}", ct).ConfigureAwait(false);
            var dto = JsonSerializer.Deserialize(json, RuntimeConfigJsonContext.Default.RuntimeConfigDto);
            if (dto is not null)
            {
                RestrictServerUrl = string.IsNullOrWhiteSpace(dto.RestrictServerUrl)
                    ? null
                    : dto.RestrictServerUrl.TrimEnd('/');
            }
        }
        catch
        {
            // Missing or malformed config.json is non-fatal — leave defaults.
        }
#else
        await Task.CompletedTask;
#endif
    }

#if __WASM__
    // Reads window.location.origin via the .NET 7+ JS interop. Returns
    // empty string when JS host isn't available (shouldn't happen on WASM).
    private static string GetWindowOrigin()
    {
        try
        {
            var location = JSHost.GlobalThis.GetPropertyAsJSObject("location");
            return location?.GetPropertyAsString("origin") ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
#endif

    internal sealed class RuntimeConfigDto
    {
        [JsonPropertyName("restrictServerUrl")]
        public string? RestrictServerUrl { get; set; }
    }

    [JsonSerializable(typeof(RuntimeConfigDto))]
    internal partial class RuntimeConfigJsonContext : JsonSerializerContext;
}
