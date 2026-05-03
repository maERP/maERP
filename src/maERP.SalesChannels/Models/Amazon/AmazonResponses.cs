using System.Text.Json.Serialization;

namespace maERP.SalesChannels.Models.Amazon;

// Minimal subset of the SP-API JSON contract we need. The full schemas are huge; we lift only
// the fields the connector reads. New fields can be added as we wire up more operations.

public sealed class AmazonLwaTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
    [JsonPropertyName("token_type")] public string TokenType { get; set; } = string.Empty;
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
}

public sealed class AmazonOrdersResponse
{
    [JsonPropertyName("payload")] public AmazonOrdersPayload? Payload { get; set; }
}

public sealed class AmazonOrdersPayload
{
    [JsonPropertyName("Orders")] public List<AmazonOrder> Orders { get; set; } = new();
    [JsonPropertyName("NextToken")] public string? NextToken { get; set; }
}

public sealed class AmazonOrder
{
    [JsonPropertyName("AmazonOrderId")] public string AmazonOrderId { get; set; } = string.Empty;
    [JsonPropertyName("PurchaseDate")] public DateTime PurchaseDate { get; set; }
    [JsonPropertyName("OrderStatus")] public string OrderStatus { get; set; } = string.Empty;
    [JsonPropertyName("OrderTotal")] public AmazonMoney? OrderTotal { get; set; }
    [JsonPropertyName("BuyerInfo")] public AmazonBuyerInfo? BuyerInfo { get; set; }
    [JsonPropertyName("ShippingAddress")] public AmazonAddress? ShippingAddress { get; set; }
}

public sealed class AmazonMoney
{
    [JsonPropertyName("CurrencyCode")] public string CurrencyCode { get; set; } = string.Empty;
    [JsonPropertyName("Amount")] public string Amount { get; set; } = "0";
}

public sealed class AmazonBuyerInfo
{
    [JsonPropertyName("BuyerEmail")] public string? BuyerEmail { get; set; }
    [JsonPropertyName("BuyerName")] public string? BuyerName { get; set; }
}

public sealed class AmazonAddress
{
    [JsonPropertyName("Name")] public string? Name { get; set; }
    [JsonPropertyName("AddressLine1")] public string? AddressLine1 { get; set; }
    [JsonPropertyName("City")] public string? City { get; set; }
    [JsonPropertyName("PostalCode")] public string? PostalCode { get; set; }
    [JsonPropertyName("CountryCode")] public string? CountryCode { get; set; }
    [JsonPropertyName("Phone")] public string? Phone { get; set; }
}
