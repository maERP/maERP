namespace maERP.Domain.Models.SalesChannelData.Shopware5;

public class Shopware5OrderResponse
{
    public int id { get; set; }
    public string? number { get; set; }
    public int customerId { get; set; }
    public int paymentId { get; set; }
    public int dispatchId { get; set; }
    public string? partnerId { get; set; }
    public int shopId { get; set; }
    public double invoiceAmount { get; set; }
    public double invoiceAmountNet { get; set; }
    public double invoiceShipping { get; set; }
    public double invoiceShippingNet { get; set; }
    public DateTime orderTime { get; set; }
    public string? transactionId { get; set; }
    public string? comment { get; set; }
    public string? customerComment { get; set; }
    public string? internalComment { get; set; }
    public int net { get; set; }
    public int taxFree { get; set; }
    public string? temporaryId { get; set; }
    public string? referer { get; set; }
    public object? clearedDate { get; set; }
    public string? trackingCode { get; set; }
    public string? languageIso { get; set; }
    public string? currency { get; set; }
    public int currencyFactor { get; set; }
    public string? remoteAddress { get; set; }
    public int paymentStatusId { get; set; }
    public int orderStatusId { get; set; }
}