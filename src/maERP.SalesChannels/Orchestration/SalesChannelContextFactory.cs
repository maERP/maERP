using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;

namespace maERP.SalesChannels.Orchestration;

/// <summary>
/// Builds a per-run <see cref="SalesChannelContext"/> from a <see cref="SalesChannel"/> entity:
/// decrypts credentials, requests the matching typed HttpClient from the factory, and pre-points
/// the client at the channel's base URL.
/// </summary>
public sealed class SalesChannelContextFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICredentialEncryptor _encryptor;

    public SalesChannelContextFactory(IHttpClientFactory httpClientFactory, ICredentialEncryptor encryptor)
    {
        _httpClientFactory = httpClientFactory;
        _encryptor = encryptor;
    }

    public SalesChannelContext Create(SalesChannel salesChannel, ChannelSyncRun syncRun, CancellationToken cancellationToken)
    {
        var clientName = HttpClientNameFor(salesChannel.Type);
        var httpClient = _httpClientFactory.CreateClient(clientName);
        httpClient.Timeout = TimeSpan.FromSeconds(60);

        return new SalesChannelContext
        {
            SalesChannel = salesChannel,
            // SalesChannel entity properties are already decrypted by the EncryptedStringConverter
            // when EF reads them, so we just pass them through. We re-decrypt only as a guard
            // against legacy plaintext rows (the encryptor passes plaintext through unchanged).
            Password = _encryptor.Decrypt(salesChannel.Password),
            AccessToken = _encryptor.Decrypt(salesChannel.AccessToken ?? string.Empty),
            RefreshToken = _encryptor.Decrypt(salesChannel.RefreshToken ?? string.Empty),
            HttpClient = httpClient,
            SyncRun = syncRun,
            TenantId = salesChannel.TenantId,
            CancellationToken = cancellationToken,
        };
    }

    public static string HttpClientNameFor(SalesChannelType type) => type switch
    {
        SalesChannelType.Shopware5 => "shopware5",
        SalesChannelType.Shopware6 => "shopware6",
        SalesChannelType.WooCommerce => "woocommerce",
        SalesChannelType.eBay => "ebay",
        SalesChannelType.Amazon => "amazon",
        _ => "default",
    };
}
