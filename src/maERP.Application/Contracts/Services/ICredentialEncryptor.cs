namespace maERP.Application.Contracts.Services;

/// <summary>
/// Encrypts/decrypts secret strings (sales-channel passwords, OAuth tokens) at rest.
/// Implementations may be a no-op (Design-Time / tests) or a real DataProtection-backed
/// encryptor (production). Decryption silently passes through values that are not in the
/// expected ciphertext format so legacy plaintext rows can be read and re-saved encrypted.
/// </summary>
public interface ICredentialEncryptor
{
    string Encrypt(string plaintext);
    string Decrypt(string ciphertext);
}
