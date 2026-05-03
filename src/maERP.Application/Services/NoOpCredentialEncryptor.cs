using maERP.Application.Contracts.Services;

namespace maERP.Application.Services;

/// <summary>
/// Identity-function encryptor used at Design-Time (EF migrations) and in unit tests
/// where no real DataProtection key ring is available.
/// </summary>
public sealed class NoOpCredentialEncryptor : ICredentialEncryptor
{
    public string Encrypt(string plaintext) => plaintext ?? string.Empty;
    public string Decrypt(string ciphertext) => ciphertext ?? string.Empty;
}
