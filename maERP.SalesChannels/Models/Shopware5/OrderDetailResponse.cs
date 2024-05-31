namespace maERP.SalesChannels.Models.Shopware5;


public class OrderDetailResponse
{
    public int id { get; set; }
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
    public string orderTime { get; set; } = string.Empty;
    public string transactionId { get; set; } = string.Empty;
    public string comment { get; set; } = string.Empty;
    public string customerComment { get; set; } = string.Empty;
    public string internalComment { get; set; } = string.Empty;
    public int net { get; set; }
    public int taxFree { get; set; }
    public string temporaryId { get; set; } = string.Empty;
    public string referer { get; set; } = string.Empty;
    public string clearedDate { get; set; } = string.Empty;
    public string trackingCode { get; set; } = string.Empty;
    public string languageIso { get; set; } = string.Empty;
    public string currency { get; set; } = string.Empty;
    public int currencyFactor { get; set; }
    public string remoteAddress { get; set; } = string.Empty;
    public List<Detail> details { get; set; } = new List<Detail>();
    // public object[] documents { get; set; } = Array.Empty<object>();
    // public Payment payment { get; set; } = new Payment();
    // public Paymentstatus paymentStatus { get; set; } = new Paymentstatus();
    // public Orderstatus orderStatus { get; set; } = new Orderstatus();
    public Customer? customer { get; set; } = new Customer();
    // public object[] paymentInstances { get; set; } = Array.Empty<object>();
    public Billing billing { get; set; } = new Billing();
    public Shipping shipping { get; set; } = new Shipping();
    // public Shop shop { get; set; } = new Shop();
    // public Dispatch dispatch { get; set; } = new Dispatch();
    // public Attribute2 attribute { get; set; } = new Attribute2();
    // public Languagesubshop languageSubShop { get; set; } = new Languagesubshop();
    // public int paymentStatusId { get; set; }
    // public int orderStatusId { get; set; }
}