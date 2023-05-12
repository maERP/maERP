namespace maERP.Shared.Models.SalesChannels.Shopware5.OrderResponse;

public class Shopware5OrderResponse
{
    public uint id { get; set; }
    public string? number { get; set; }
    public uint customerId { get; set; }
    public uint paymentId { get; set; }
    public uint dispatchId { get; set; }
    public string? partnerId { get; set; }
    public uint shopId { get; set; }
    public double invoiceAmount { get; set; }
    public double invoiceAmountNet { get; set; }
    public double invoiceShipping { get; set; }
    public double invoiceShippingNet { get; set; }
    public DateTime orderTime { get; set; }
    public string? transactionId { get; set; }
    public string? comment { get; set; }
    public string? customerComment { get; set; }
    public string? internalComment { get; set; }
    public uint net { get; set; }
    public uint taxFree { get; set; }
    public string? temporaryId { get; set; }
    public string? referer { get; set; }
    public object? clearedDate { get; set; }
    public string? trackingCode { get; set; }
    public string? languageIso { get; set; }
    public string? currency { get; set; }
    public uint currencyFactor { get; set; }
    public string? remoteAddress { get; set; }
    public uint paymentStatusId { get; set; }
    public uint orderStatusId { get; set; }
}