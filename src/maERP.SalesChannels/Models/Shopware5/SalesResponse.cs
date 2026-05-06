namespace maERP.SalesChannels.Models.Shopware5;

public class SalesResponse
{
    public int id { get; set; }
    // public DateTime changed { get; set; }
    public string number { get; set; } = string.Empty;
    public int customerId { get; set; }
    public int paymentId { get; set; }
    public int dispatchId { get; set; }
    public string partnerId { get; set; } = string.Empty;
    public int shopId { get; set; }
    public decimal invoiceAmount { get; set; }
    public decimal invoiceAmountNet { get; set; }
    public decimal invoiceShipping { get; set; }
    public decimal invoiceShippingNet { get; set; }
    public object invoiceShippingTaxRate { get; set; } = new object();
    // public DateTime salesTime { get; set; }
    public string transactionId { get; set; } = string.Empty;
    public string comment { get; set; } = string.Empty;
    public string customerComment { get; set; } = string.Empty;
    public string internalComment { get; set; } = string.Empty;
    public int net { get; set; }
    public int taxFree { get; set; }
    public string temporaryId { get; set; } = string.Empty;
    public string referer { get; set; } = string.Empty;
    // public DateTime? clearedDate { get; set; }
    public string trackingCode { get; set; } = string.Empty;
    public string languageIso { get; set; } = string.Empty;
    public string currency { get; set; } = string.Empty;
    public int currencyFactor { get; set; }
    public string remoteAddress { get; set; } = string.Empty;
    public string deviceType { get; set; } = string.Empty;
    public bool isProportionalCalculation { get; set; }
    public Shopware5SalesAttribute attribute { get; set; } = new Shopware5SalesAttribute();
    public Shopware5SalesCustomer customer { get; set; } = new Shopware5SalesCustomer();
    public int paymentStatusId { get; set; }
    public int salesStatusId { get; set; }
}

public class Shopware5SalesCustomer
{
    public int id { get; set; }
    public string email { get; set; } = string.Empty;
}

public class Shopware5SalesAttribute
{
    public int id { get; set; }
    public int salesId { get; set; }
    public string attribute1 { get; set; } = string.Empty;
    public string attribute2 { get; set; } = string.Empty;
    public string attribute3 { get; set; } = string.Empty;
    public string attribute4 { get; set; } = string.Empty;
    public string attribute5 { get; set; } = string.Empty;
    public string attribute6 { get; set; } = string.Empty;
    public object swagDhlAddress { get; set; } = new object();
    public object swagDhlSalesInfo { get; set; } = new object();
    public bool swagDhlSalesLabelPrinted { get; set; }
    public object bestitAmazonCaptureId { get; set; } = new object();
    public object bestitAmazonCaptureReference { get; set; } = new object();
    public object bestitAmazonAuthorizationId { get; set; } = new object();
    public object bestitAmazonAuthorisationReference { get; set; } = new object();
    public object bestitAmazonRefundId { get; set; } = new object();
    public object bestitAmazonRefundReference { get; set; } = new object();
    public object bestitAmazonCancel { get; set; } = new object();
    public object bestitAmazonInvoiceAmount { get; set; } = new object();
    public object bestitAmazonSimulationCode { get; set; } = new object();
    public object swagPaypalUnifiedPaymentType { get; set; } = new object();
    public object swagPayalBillingAgreementId { get; set; } = new object();
    public object swagPayalExpress { get; set; } = new object();
}