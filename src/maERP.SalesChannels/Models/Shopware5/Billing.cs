namespace maERP.SalesChannels.Models.Shopware5;

public class Billing
{
    public int id { get; set; }
    public int orderId { get; set; }
    public int customerId { get; set; }
    public int countryId { get; set; }
    public string company { get; set; } = string.Empty;
    public string department { get; set; } = string.Empty;
    public string salutation { get; set; } = string.Empty;
    public string number { get; set; } = string.Empty;
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string street { get; set; } = string.Empty;
    public string zipCode { get; set; } = string.Empty;
    public string city { get; set; } = string.Empty;
    public string phone { get; set; } = string.Empty;
    public string fax { get; set; } = string.Empty;
    public string vatId { get; set; } = string.Empty;
    public Country country { get; set; } = new Country();
    // public Attribute1 attribute { get; set; } = new Attribute1();
}
