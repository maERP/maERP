namespace maERP.SalesChannels.Models.Shopware5;

public class Dispatch
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public int type { get; set; }
    public string description { get; set; } = string.Empty;
    public string comment { get; set; } = string.Empty;
    public bool active { get; set; }
    public int position { get; set; }
    public int calculation { get; set; }
    public int surchargeCalculation { get; set; }
    public int taxCalculation { get; set; }
    public object shippingFree { get; set; } = new object();
    public object multiShopId { get; set; } = new object();
    public object customerGroupId { get; set; } = new object();
    public int bindShippingFree { get; set; }
    public object bindTimeFrom { get; set; } = new object();
    public object bindTimeTo { get; set; } = new object();
    public object bindInStock { get; set; } = new object();
    public int bindLastStock { get; set; }
    public object bindWeekdayFrom { get; set; } = new object();
    public object bindWeekdayTo { get; set; } = new object();
    public object bindWeightFrom { get; set; } = new object();
    public string bindWeightTo { get; set; } = string.Empty;
    public object bindPriceFrom { get; set; } = new object();
    public object bindPriceTo { get; set; } = new object();
    public object bindSql { get; set; } = new object();
    public string statusLink { get; set; } = string.Empty;
    public object calculationSql { get; set; } = new object();
}
