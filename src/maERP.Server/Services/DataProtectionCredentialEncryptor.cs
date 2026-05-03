using maERP.Application.Contracts.Services;
using Microsoft.AspNetCore.DataProtection;

namespace maERP.Server.Services;

/// <summary>
/// Production credential encryptor backed by ASP.NET Core DataProtection. Lives in the
/// Server layer because <c>Microsoft.AspNetCore.DataProtection</c> is part of the
/// AspNetCore shared framework — class libraries (Persistence, Infrastructure) would have
/// to take an extra NuGet reference. The Server already has it for free.
/// </summary>
public sealed class DataProtectionCredentialEncryptor : ICredentialEncryptor
{
    public const string Purpose = "saleschannel:credentials:v1";

    private readonly IDataProtector _protector;

    public DataProtectionCredentialEncryptor(IDataProtectionProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);
        _protector = provider.CreateProtector(Purpose);
    }

    public string Encrypt(string plaintext)
    {
        if (string.IsNullOrEmpty(plaintext))
        {
            return plaintext ?? string.Empty;
        }
        return _protector.Protect(plaintext);
    }

    public string Decrypt(string ciphertext)
    {
        if (string.IsNullOrEmpty(ciphertext))
        {
            return ciphertext ?? string.Empty;
        }

        try
        {
            return _protector.Unprotect(ciphertext);
        }
        catch (System.Security.Cryptography.CryptographicException)
        {
            // Legacy plaintext row from before encryption was rolled out — pass through.
            // The next save will re-encrypt it.
            return ciphertext;
        }
        catch (FormatException)
        {
            return ciphertext;
        }
    }
}
