namespace maERP.Shared.Models.SalesChannels.Shopware5.ProductResponse;

public class Shopware5ProductResponse
{
    public uint id { get; set; }
    public uint mainDetailId { get; set; }
    public uint supplierId { get; set; }
    public uint taxId { get; set; }
    public object? priceGroupId { get; set; }
    public object? filterGroupId { get; set; }
    public object? configuratorSetId { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public string? descriptionLong { get; set; }
    public DateTime added { get; set; }
    public bool active { get; set; }
    public uint pseudoSales { get; set; }
    public bool highlight { get; set; }
    public string? keywords { get; set; }
    public string? metaTitle { get; set; }
    public DateTime changed { get; set; }
    public bool priceGroupActive { get; set; }
    public bool lastStock { get; set; }
    public uint crossBundleLook { get; set; }
    public bool notification { get; set; }
    public string? template { get; set; }
    public uint mode { get; set; }
    public object? availableFrom { get; set; }
    public object? availableTo { get; set; }
    public MainDetail? mainDetail { get; set; }
}

public class MainDetail
{
    public uint id { get; set; }
    public uint articleId { get; set; }
    public uint unitId { get; set; }
    public string? number { get; set; }
    public string? supplierNumber { get; set; }
    public uint kind { get; set; }
    public string? additionalText { get; set; }
    public bool active { get; set; }
    public uint inStock { get; set; }
    public uint stockMin { get; set; }
    public bool lastStock { get; set; }
    public string? weight { get; set; }
    public string? width { get; set; }
    public string? len { get; set; }
    public string? height { get; set; }
    public string? ean { get; set; }
    public double purchasePrice { get; set; }
    public uint position { get; set; }
    public uint minPurchase { get; set; }
    public uint purchaseSteps { get; set; }
    public object? maxPurchase { get; set; }
    public string? purchaseUnit { get; set; }
    public string? referenceUnit { get; set; }
    public string? packUnit { get; set; }
    public bool? shippingFree { get; set; }
    public object? releaseDate { get; set; }
    public string? shippingTime { get; set; }
    public Attribute? attribute { get; set; }
}

public class Attribute
{
    public uint id { get; set; }
    public uint articleDetailId { get; set; }
    public object? attr1 { get; set; }
    public object? attr2 { get; set; }
    public object? attr3 { get; set; }
    public object? attr4 { get; set; }
    public object? attr5 { get; set; }
    public object? attr6 { get; set; }
    public object? attr7 { get; set; }
    public object? attr8 { get; set; }
    public object? attr9 { get; set; }
    public object? attr10 { get; set; }
    public object? attr11 { get; set; }
    public object? attr12 { get; set; }
    public object? attr13 { get; set; }
    public object? attr14 { get; set; }
    public object? attr15 { get; set; }
    public object? attr16 { get; set; }
    public object? attr17 { get; set; }
    public object? attr18 { get; set; }
    public object? attr19 { get; set; }
    public object? attr20 { get; set; }
    public object? swagIsTrustedShopsArticle { get; set; }
    public object? swagTrustedRange { get; set; }
    public object? swagTrustedDuration { get; set; }
}