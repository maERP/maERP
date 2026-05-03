using System.Text.Json;
using System.Text.Json.Serialization;
using maERP.Domain.Entities;

namespace maERP.SalesChannels.Models.Amazon;

/// <summary>
/// Strongly-typed slice of <c>SalesChannel.AdditionalConfigJson</c> for Amazon. The connector
/// validates this on first use; missing fields cause a Failed connection test instead of a
/// runtime null-ref deep in the API call.
/// </summary>
public sealed class AmazonChannelConfig
{
    /// <summary>LWA app client id (Amazon Seller Central → Develop apps → "Login with Amazon credentials").</summary>
    [JsonPropertyName("lwaClientId")]
    public string LwaClientId { get; set; } = string.Empty;

    /// <summary>LWA app client secret. Stored encrypted at rest via the same DataProtection key ring.</summary>
    [JsonPropertyName("lwaClientSecret")]
    public string LwaClientSecret { get; set; } = string.Empty;

    /// <summary>Amazon Seller ID (the merchant token).</summary>
    [JsonPropertyName("sellerId")]
    public string SellerId { get; set; } = string.Empty;

    /// <summary>Region: "na", "eu", or "fe". Drives the SP-API endpoint base URL.</summary>
    [JsonPropertyName("region")]
    public string Region { get; set; } = "eu";

    /// <summary>Use the SP-API sandbox endpoint instead of production.</summary>
    [JsonPropertyName("useSandbox")]
    public bool UseSandbox { get; set; }

    public static AmazonChannelConfig FromSalesChannel(SalesChannel salesChannel)
    {
        if (string.IsNullOrEmpty(salesChannel.AdditionalConfigJson))
        {
            return new AmazonChannelConfig();
        }
        return JsonSerializer.Deserialize<AmazonChannelConfig>(salesChannel.AdditionalConfigJson)
               ?? new AmazonChannelConfig();
    }

    public string GetEndpointBaseUrl()
    {
        var hostByRegion = Region.ToLowerInvariant() switch
        {
            "na" => UseSandbox ? "sandbox.sellingpartnerapi-na.amazon.com" : "sellingpartnerapi-na.amazon.com",
            "fe" => UseSandbox ? "sandbox.sellingpartnerapi-fe.amazon.com" : "sellingpartnerapi-fe.amazon.com",
            _ => UseSandbox ? "sandbox.sellingpartnerapi-eu.amazon.com" : "sellingpartnerapi-eu.amazon.com",
        };
        return $"https://{hostByRegion}";
    }
}
