using maERP.SalesChannels.Models;

namespace maERP.SalesChannels.Contracts;

public interface IOrderImportRepository
{
    Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportOrder importOrder);
}