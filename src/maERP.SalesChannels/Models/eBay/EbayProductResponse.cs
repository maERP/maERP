#nullable disable

using System.Text.Json.Serialization;

namespace maERP.SalesChannels.Models.eBay;

public class EbayInventoryItemResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    [JsonPropertyName("inventoryItems")]
    public EbayInventoryItem[] InventoryItems { get; set; }
}

public class EbayInventoryItem
{
    [JsonPropertyName("sku")]
    public string Sku { get; set; }
    
    [JsonPropertyName("availability")]
    public EbayAvailability Availability { get; set; }
    
    [JsonPropertyName("product")]
    public EbayProduct Product { get; set; }
}

public class EbayAvailability
{
    [JsonPropertyName("shipToLocationAvailability")]
    public EbayShipToLocationAvailability ShipToLocationAvailability { get; set; }
}

public class EbayShipToLocationAvailability
{
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}

public class EbayProduct
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("aspects")]
    public Dictionary<string, string[]> Aspects { get; set; }
    
    [JsonPropertyName("imageUrls")]
    public string[] ImageUrls { get; set; }
    
    [JsonPropertyName("mpn")]
    public string Mpn { get; set; }
    
    [JsonPropertyName("ean")]
    public string[] Ean { get; set; }
    
    [JsonPropertyName("upc")]
    public string[] Upc { get; set; }
}

public class EbayOfferResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    [JsonPropertyName("offers")]
    public EbayOffer[] Offers { get; set; }
}

public class EbayOffer
{
    [JsonPropertyName("offerId")]
    public string OfferId { get; set; }
    
    [JsonPropertyName("sku")]
    public string Sku { get; set; }
    
    [JsonPropertyName("marketplaceId")]
    public string MarketplaceId { get; set; }
    
    [JsonPropertyName("format")]
    public string Format { get; set; }
    
    [JsonPropertyName("pricingSummary")]
    public EbayOfferPricingSummary PricingSummary { get; set; }
    
    [JsonPropertyName("listingPolicies")]
    public EbayListingPolicies ListingPolicies { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
}

public class EbayOfferPricingSummary
{
    [JsonPropertyName("price")]
    public EbayAmount Price { get; set; }
}

public class EbayListingPolicies
{
    [JsonPropertyName("fulfillmentPolicies")]
    public EbayFulfillmentPolicies FulfillmentPolicies { get; set; }
    
    [JsonPropertyName("paymentPolicies")]
    public EbayPaymentPolicies PaymentPolicies { get; set; }
    
    [JsonPropertyName("returnPolicies")]
    public EbayReturnPolicies ReturnPolicies { get; set; }
}

public class EbayFulfillmentPolicies
{
    [JsonPropertyName("shippingOptions")]
    public EbayShippingOption[] ShippingOptions { get; set; }
}

public class EbayShippingOption
{
    [JsonPropertyName("costType")]
    public string CostType { get; set; }
    
    [JsonPropertyName("shippingServices")]
    public EbayShippingService[] ShippingServices { get; set; }
}

public class EbayShippingService
{
    [JsonPropertyName("shippingCarrierCode")]
    public string ShippingCarrierCode { get; set; }
    
    [JsonPropertyName("shippingServiceCode")]
    public string ShippingServiceCode { get; set; }
    
    [JsonPropertyName("shippingCost")]
    public EbayAmount ShippingCost { get; set; }
}

public class EbayPaymentPolicies
{
    [JsonPropertyName("paymentMethods")]
    public string[] PaymentMethods { get; set; }
}

public class EbayReturnPolicies
{
    [JsonPropertyName("returnsAccepted")]
    public bool ReturnsAccepted { get; set; }
    
    [JsonPropertyName("returnPeriod")]
    public EbayReturnPeriod ReturnPeriod { get; set; }
    
    [JsonPropertyName("returnType")]
    public string ReturnType { get; set; }
}

public class EbayReturnPeriod
{
    [JsonPropertyName("unit")]
    public string Unit { get; set; }
    
    [JsonPropertyName("value")]
    public int Value { get; set; }
} 