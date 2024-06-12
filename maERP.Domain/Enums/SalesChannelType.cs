using System.ComponentModel;

namespace maERP.Domain.Models;

public enum SalesChannelType
{
    [Description("Kasse")]
    PointOfSale = 1,

    [Description("Shopware 5")]
    Shopware5 = 10,

    [Description("Shopware 6")]
    Shopware6 = 11,

    [Description("WooCommerce")]
    WooCommerce = 20
}