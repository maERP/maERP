using System.ComponentModel;

namespace maERP.SharedUI.Models.SalesChannel;

public enum SalesChannelType
{
    [Description("Kein Typ")]
    NoType,
    
    [Description("Kasse")]
    PointOfSale,

    [Description("Shopware 5")]
    Shopware5,

    [Description("Shopware 6")]
    Shopware6,

    [Description("WooCommerce")]
    WooCommerce
}