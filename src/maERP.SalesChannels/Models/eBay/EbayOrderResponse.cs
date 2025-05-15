#nullable disable

using System.Text.Json.Serialization;

namespace maERP.SalesChannels.Models.eBay;

public class EbayOrderResponse
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; }
    
    [JsonPropertyName("legacyOrderId")]
    public string LegacyOrderId { get; set; }
    
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }
    
    [JsonPropertyName("lastModifiedDate")]
    public DateTime LastModifiedDate { get; set; }
    
    [JsonPropertyName("orderFulfillmentStatus")]
    public string OrderFulfillmentStatus { get; set; }
    
    [JsonPropertyName("orderPaymentStatus")]
    public string OrderPaymentStatus { get; set; }
    
    [JsonPropertyName("sellerId")]
    public string SellerId { get; set; }
    
    [JsonPropertyName("buyer")]
    public EbayBuyer Buyer { get; set; }
    
    [JsonPropertyName("pricingSummary")]
    public EbayPricingSummary PricingSummary { get; set; }
    
    [JsonPropertyName("fulfillmentStartInstructions")]
    public EbayFulfillmentStartInstruction[] FulfillmentStartInstructions { get; set; }
    
    [JsonPropertyName("lineItems")]
    public EbayLineItem[] LineItems { get; set; }
    
    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class EbayBuyer
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("taxAddress")]
    public EbayAddress TaxAddress { get; set; }
}

public class EbayAddress
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    
    [JsonPropertyName("addressLine1")]
    public string AddressLine1 { get; set; }
    
    [JsonPropertyName("addressLine2")]
    public string AddressLine2 { get; set; }
    
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }
    
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }
    
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}

public class EbayPricingSummary
{
    [JsonPropertyName("subtotal")]
    public EbayAmount Subtotal { get; set; }
    
    [JsonPropertyName("total")]
    public EbayAmount Total { get; set; }
    
    [JsonPropertyName("totalTaxAmount")]
    public EbayAmount TotalTaxAmount { get; set; }
}

public class EbayAmount
{
    [JsonPropertyName("value")]
    public decimal Value { get; set; }
    
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}

public class EbayFulfillmentStartInstruction
{
    [JsonPropertyName("fulfillmentInstructionsType")]
    public string FulfillmentInstructionsType { get; set; }
    
    [JsonPropertyName("shippingStep")]
    public EbayShippingStep ShippingStep { get; set; }
}

public class EbayShippingStep
{
    [JsonPropertyName("shippingCarrierCode")]
    public string ShippingCarrierCode { get; set; }
    
    [JsonPropertyName("shippingServiceCode")]
    public string ShippingServiceCode { get; set; }
    
    [JsonPropertyName("shipTo")]
    public EbayAddress ShipTo { get; set; }
}

public class EbayLineItem
{
    [JsonPropertyName("lineItemId")]
    public string LineItemId { get; set; }
    
    [JsonPropertyName("legacyItemId")]
    public string LegacyItemId { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("sku")]
    public string Sku { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [JsonPropertyName("lineItemCost")]
    public EbayAmount LineItemCost { get; set; }
} 