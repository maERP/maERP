namespace maERP.SalesChannels.Models.Shopware5;

public class Debit
{
    public int id { get; set; }
    public int customerId { get; set; }
    public string account { get; set; } = string.Empty;
    public string bankCode { get; set; } = string.Empty;
    public string bankName { get; set; } = string.Empty;
    public string accountHolder { get; set; } = string.Empty;
}
