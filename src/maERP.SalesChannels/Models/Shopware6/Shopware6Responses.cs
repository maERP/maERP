using System.Text.Json.Serialization;

namespace maERP.SalesChannels.Models.Shopware6;

// Minimal Shopware 6 Admin-API DTOs. We lift just the fields the connector reads/writes;
// new ones can be added without breaking serialization (System.Text.Json ignores extras).

public sealed class Sw6OAuthTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("token_type")] public string TokenType { get; set; } = string.Empty;
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
}

public sealed class Sw6SearchResult<T>
{
    [JsonPropertyName("data")] public List<T> Data { get; set; } = new();
    [JsonPropertyName("total")] public int Total { get; set; }
}

public sealed class Sw6Product
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("productNumber")] public string? ProductNumber { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("ean")] public string? Ean { get; set; }
    [JsonPropertyName("manufacturerNumber")] public string? ManufacturerNumber { get; set; }
    [JsonPropertyName("stock")] public int? Stock { get; set; }
    [JsonPropertyName("price")] public List<Sw6Price> Price { get; set; } = new();
    [JsonPropertyName("translated")] public Sw6Translated? Translated { get; set; }
}

public sealed class Sw6Translated
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}

public sealed class Sw6Price
{
    [JsonPropertyName("gross")] public decimal Gross { get; set; }
    [JsonPropertyName("net")] public decimal Net { get; set; }
    [JsonPropertyName("currencyId")] public string? CurrencyId { get; set; }
}

public sealed class Sw6Sales
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("salesNumber")] public string? SalesNumber { get; set; }
    [JsonPropertyName("salesDateTime")] public DateTime SalesDateTime { get; set; }
    [JsonPropertyName("amountTotal")] public decimal AmountTotal { get; set; }
    [JsonPropertyName("amountNet")] public decimal AmountNet { get; set; }
    [JsonPropertyName("shippingTotal")] public decimal ShippingTotal { get; set; }
    [JsonPropertyName("salesCustomer")] public Sw6SalesCustomer? SalesCustomer { get; set; }
    [JsonPropertyName("billingAddress")] public Sw6Address? BillingAddress { get; set; }
    [JsonPropertyName("deliveries")] public List<Sw6Delivery> Deliveries { get; set; } = new();
    [JsonPropertyName("lineItems")] public List<Sw6LineItem> LineItems { get; set; } = new();
    [JsonPropertyName("stateMachineState")] public Sw6StateMachineState? StateMachineState { get; set; }
}

public sealed class Sw6SalesCustomer
{
    [JsonPropertyName("email")] public string? Email { get; set; }
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    [JsonPropertyName("company")] public string? Company { get; set; }
    [JsonPropertyName("customerId")] public string? CustomerId { get; set; }
}

public sealed class Sw6Address
{
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    [JsonPropertyName("company")] public string? Company { get; set; }
    [JsonPropertyName("street")] public string? Street { get; set; }
    [JsonPropertyName("zipcode")] public string? Zipcode { get; set; }
    [JsonPropertyName("city")] public string? City { get; set; }
    [JsonPropertyName("phoneNumber")] public string? PhoneNumber { get; set; }
    [JsonPropertyName("country")] public Sw6Country? Country { get; set; }
}

public sealed class Sw6Country
{
    [JsonPropertyName("iso")] public string? Iso { get; set; }
}

public sealed class Sw6Delivery
{
    [JsonPropertyName("shippingSalesAddress")] public Sw6Address? ShippingSalesAddress { get; set; }
}

public sealed class Sw6LineItem
{
    [JsonPropertyName("label")] public string? Label { get; set; }
    [JsonPropertyName("payload")] public Sw6LineItemPayload? Payload { get; set; }
    [JsonPropertyName("quantity")] public int Quantity { get; set; }
    [JsonPropertyName("unitPrice")] public decimal UnitPrice { get; set; }
    [JsonPropertyName("totalPrice")] public decimal TotalPrice { get; set; }
}

public sealed class Sw6LineItemPayload
{
    [JsonPropertyName("productNumber")] public string? ProductNumber { get; set; }
    [JsonPropertyName("ean")] public string? Ean { get; set; }
    [JsonPropertyName("taxId")] public string? TaxId { get; set; }
}

public sealed class Sw6StateMachineState
{
    [JsonPropertyName("technicalName")] public string? TechnicalName { get; set; }
}
