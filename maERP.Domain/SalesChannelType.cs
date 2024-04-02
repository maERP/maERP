using System.ComponentModel;

namespace maERP.Domain;

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