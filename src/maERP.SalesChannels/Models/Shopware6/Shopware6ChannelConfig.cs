using System.Text.Json;
using System.Text.Json.Serialization;
using maERP.Domain.Entities;

namespace maERP.SalesChannels.Models.Shopware6;

/// <summary>
/// Strongly-typed slice of <c>SalesChannel.AdditionalConfigJson</c> for Shopware 6. Auth is via
/// the Admin API's OAuth client_credentials flow; the user pastes their <c>API access ID</c> and
/// <c>API secret access key</c> from Shopware → Settings → System → Integrations into
/// <see cref="ApiClientId"/> / <see cref="ApiClientSecret"/>. <c>SalesChannel.Username</c> +
/// <c>Password</c> remain unused for Shopware 6.
/// </summary>
public sealed class Shopware6ChannelConfig
{
    [JsonPropertyName("apiClientId")]
    public string ApiClientId { get; set; } = string.Empty;

    [JsonPropertyName("apiClientSecret")]
    public string ApiClientSecret { get; set; } = string.Empty;

    /// <summary>Optional language id (UUID) — empty falls back to the shop's default.</summary>
    [JsonPropertyName("languageId")]
    public string? LanguageId { get; set; }

    public static Shopware6ChannelConfig FromSalesChannel(SalesChannel salesChannel)
    {
        if (string.IsNullOrEmpty(salesChannel.AdditionalConfigJson))
        {
            // Fall back to Username/Password for installations that haven't migrated to AdditionalConfigJson yet.
            return new Shopware6ChannelConfig
            {
                ApiClientId = salesChannel.Username,
                ApiClientSecret = salesChannel.Password,
            };
        }
        return JsonSerializer.Deserialize<Shopware6ChannelConfig>(salesChannel.AdditionalConfigJson)
               ?? new Shopware6ChannelConfig();
    }
}
