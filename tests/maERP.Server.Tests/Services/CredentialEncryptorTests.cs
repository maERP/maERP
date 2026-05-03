using maERP.Application.Services;
using maERP.Server.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Services;

public class CredentialEncryptorTests
{
    private static DataProtectionCredentialEncryptor CreateProductionEncryptor()
    {
        var services = new ServiceCollection();
        services.AddDataProtection().SetApplicationName("test-app");
        var provider = services.BuildServiceProvider();
        return new DataProtectionCredentialEncryptor(provider.GetRequiredService<IDataProtectionProvider>());
    }

    [Fact]
    public void NoOp_RoundTrip_PreservesValue()
    {
        var enc = new NoOpCredentialEncryptor();
        const string plaintext = "super-secret-password";

        var cipher = enc.Encrypt(plaintext);
        var roundtrip = enc.Decrypt(cipher);

        Assert.Equal(plaintext, cipher);
        Assert.Equal(plaintext, roundtrip);
    }

    [Fact]
    public void Production_RoundTrip_PreservesValue()
    {
        var enc = CreateProductionEncryptor();
        const string plaintext = "super-secret-token";

        var cipher = enc.Encrypt(plaintext);
        var roundtrip = enc.Decrypt(cipher);

        Assert.Equal(plaintext, roundtrip);
    }

    [Fact]
    public void Production_Encrypt_ProducesCiphertextDifferentFromPlaintext()
    {
        var enc = CreateProductionEncryptor();
        const string plaintext = "shopware-api-key-12345";

        var cipher = enc.Encrypt(plaintext);

        Assert.NotEqual(plaintext, cipher);
        Assert.True(cipher.Length > plaintext.Length);
    }

    [Fact]
    public void Production_Decrypt_PassesThroughLegacyPlaintext()
    {
        // Pre-encryption-rollout rows are stored as plaintext. Decrypt() must not throw —
        // it should return the value unchanged so the next save can re-encrypt it.
        var enc = CreateProductionEncryptor();
        const string legacyPlaintext = "this-was-saved-before-encryption-was-on";

        var result = enc.Decrypt(legacyPlaintext);

        Assert.Equal(legacyPlaintext, result);
    }

    [Fact]
    public void Production_Encrypt_Empty_ReturnsEmpty()
    {
        var enc = CreateProductionEncryptor();

        Assert.Equal(string.Empty, enc.Encrypt(string.Empty));
        Assert.Equal(string.Empty, enc.Encrypt(null!));
    }

    [Fact]
    public void Production_Decrypt_Empty_ReturnsEmpty()
    {
        var enc = CreateProductionEncryptor();

        Assert.Equal(string.Empty, enc.Decrypt(string.Empty));
        Assert.Equal(string.Empty, enc.Decrypt(null!));
    }

    [Fact]
    public void Production_DistinctEncryptors_WithSameKeyRing_CanReadEachOthersOutput()
    {
        // Same DI container → same key ring → second encryptor can decrypt the first's ciphertext.
        var services = new ServiceCollection();
        services.AddDataProtection().SetApplicationName("test-app");
        var provider = services.BuildServiceProvider();

        var encA = new DataProtectionCredentialEncryptor(provider.GetRequiredService<IDataProtectionProvider>());
        var encB = new DataProtectionCredentialEncryptor(provider.GetRequiredService<IDataProtectionProvider>());

        const string plaintext = "consistent-across-instances";
        var cipher = encA.Encrypt(plaintext);
        var roundtrip = encB.Decrypt(cipher);

        Assert.Equal(plaintext, roundtrip);
    }
}
