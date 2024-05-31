namespace maERP.SalesChannels.Models.Shopware5;

public class Customer
{
    public int id { get; set; }
    public int paymentId { get; set; }
    public string groupKey { get; set; } = string.Empty;
    public int shopId { get; set; }
    public object priceGroupId { get; set; } = new object();
    public string encoderName { get; set; } = string.Empty;
    public string hashPassword { get; set; } = string.Empty;
    public bool active { get; set; }
    public string email { get; set; } = string.Empty;
    public string firstLogin { get; set; } = string.Empty;
    public string lastLogin { get; set; } = string.Empty;
    public int accountMode { get; set; }
    public string confirmationKey { get; set; } = string.Empty;
    public string sessionId { get; set; } = string.Empty;
    public int newsletter { get; set; }
    public string validation { get; set; } = string.Empty;
    public int affiliate { get; set; }
    public int paymentPreset { get; set; }
    public string languageId { get; set; } = string.Empty;
    public string referer { get; set; } = string.Empty;
    public string internalComment { get; set; } = string.Empty;
    public int failedLogins { get; set; }
    // public object lockedUntil { get; set; } = new object();
    // public Debit debit { get; set; } = new Debit();
}
