namespace maERP.SalesChannels.Models.Shopware5;

public class Paymentstatus
{
    public int id { get; set; }
    public string description { get; set; } = string.Empty;
    public int position { get; set; }
    public string group { get; set; } = string.Empty;
    public int sendMail { get; set; }
}
