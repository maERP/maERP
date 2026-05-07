namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Abstraction over per-platform secure credential storage. Implementations should prefer the
/// most-secure store available on the target platform — OS keychain / credential vault on
/// Desktop &amp; Mobile, falling back to <c>ApplicationData.Current.LocalSettings</c> on WASM.
///
/// Used to persist long-lived refresh tokens. The plaintext access (JWT) token can stay in
/// regular settings — it's short-lived. The refresh token, however, grants weeks of access on
/// presentation, so it deserves the strongest local store the platform offers.
///
/// Multiple implementations may be plugged in via DI (the registration in <c>AuthModule</c>
/// picks the right one for the current target). Consumers should not assume which backing
/// store is used.
/// </summary>
public interface ISecureCredentialStore
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, string value);
    Task RemoveAsync(string key);
}
