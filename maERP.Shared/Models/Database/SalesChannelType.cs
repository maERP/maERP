using System.ComponentModel;

namespace maERP.Shared.Models.Database;

public enum SalesChannelType
{
    [Description("Kein Typ")]
    notype,

    [Description("Shopware 5")]
    shopware5,

    [Description("Shopware 6")]
    shopware6,

    [Description("WooCommerce")]
    woocommerce
}