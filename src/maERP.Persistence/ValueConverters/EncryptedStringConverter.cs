using maERP.Application.Contracts.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace maERP.Persistence.ValueConverters;

/// <summary>
/// EF value converter that runs <see cref="ICredentialEncryptor"/> on the way to and from the
/// database. Used for SalesChannel credentials (Password, AccessToken, RefreshToken).
/// </summary>
public class EncryptedStringConverter : ValueConverter<string, string>
{
    public EncryptedStringConverter(ICredentialEncryptor encryptor)
        : base(plain => encryptor.Encrypt(plain), cipher => encryptor.Decrypt(cipher))
    {
    }
}
