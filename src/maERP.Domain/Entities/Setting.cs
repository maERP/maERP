using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Setting : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Per-row opt-in for at-rest encryption. Rows with <c>IsEncrypted = true</c> have their
    /// <see cref="Value"/> encrypted via <c>ICredentialEncryptor</c> on read/write paths that
    /// go through <c>SettingsService.GetEncryptedSettingValueAsync</c> and friends. The plain
    /// <c>Value</c> property is left unencrypted in the column to keep the existing read/write
    /// paths for non-secret rows untouched.
    /// </summary>
    public bool IsEncrypted { get; set; }
}