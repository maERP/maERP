using maERP.Application.Models.Email;
using maERP.Application.Models.Grafana;
using maERP.Application.Models.Identity;
using maERP.Application.Models.Telemetry;

namespace maERP.Application.Contracts.Persistence;

public interface ISettingsService
{
    Task<JwtSettings> GetJwtSettingsAsync();
    Task<EmailSettings> GetEmailSettingsAsync();
    Task<TelemetrySettings> GetTelemetrySettingsAsync();
    Task<GrafanaSettings> GetGrafanaSettingsAsync();
    Task<string> GetSettingValueAsync(string key);
    Task SetSettingValueAsync(string key, string value);

    /// <summary>
    /// Reads the setting value and decrypts it via <c>ICredentialEncryptor</c> if the row is
    /// flagged <c>IsEncrypted = true</c>. Plain rows pass through unchanged. Returns
    /// <see cref="string.Empty"/> when the key is not present.
    /// </summary>
    Task<string> GetEncryptedSettingValueAsync(string key);

    /// <summary>
    /// Writes the value (encrypting it via <c>ICredentialEncryptor</c>) and flags the row as
    /// <c>IsEncrypted = true</c>. Subsequent reads via <see cref="GetEncryptedSettingValueAsync"/>
    /// will decrypt; reads via <see cref="GetSettingValueAsync"/> still return the ciphertext —
    /// callers for OAuth-secret keys must use the encrypted variant.
    /// </summary>
    Task SetEncryptedSettingValueAsync(string key, string value);
}