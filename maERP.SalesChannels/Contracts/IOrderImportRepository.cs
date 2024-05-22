using maERP.Domain.Models;
using maERP.SalesChannels.Models;

namespace maERP.SalesChannels.Contracts;

public interface IOrderImportRepository
{
    Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportOrder importOrder);
}