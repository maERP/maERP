using Windows.Storage;

namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Default <see cref="ISecureCredentialStore"/> backing — uses
/// <c>ApplicationData.Current.LocalSettings</c>. The actual on-disk format is platform-specific:
///
/// <list type="bullet">
///   <item>Windows / WinUI: per-user AppContainer LocalSettings (ACL-protected).</item>
///   <item>WASM: <c>localStorage</c> for the origin (browser-managed).</item>
///   <item>Skia/Desktop &amp; Mobile: isolated storage under the per-user app folder.</item>
/// </list>
///
/// This is "as secure as the OS lets us be without bringing extra dependencies." For full
/// keychain integration on Desktop and Mobile, a platform-specific implementation can replace
/// this one in <c>AuthModule</c> behind <c>#if WINDOWS</c> / <c>#if ANDROID</c> / etc.
/// </summary>
public sealed class LocalSettingsCredentialStore : ISecureCredentialStore
{
    public Task<string?> GetAsync(string key)
    {
        var values = ApplicationData.Current.LocalSettings.Values;
        var value = values.TryGetValue(key, out var raw) ? raw?.ToString() : null;
        return Task.FromResult(value);
    }

    public Task SetAsync(string key, string value)
    {
        ApplicationData.Current.LocalSettings.Values[key] = value;
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        ApplicationData.Current.LocalSettings.Values.Remove(key);
        return Task.CompletedTask;
    }
}
