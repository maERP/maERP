using maERP.SalesChannels.Models;

namespace maERP.SalesChannels.Contracts;

public interface IProductImportRepository
{
    Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportProduct importProduct);
}
