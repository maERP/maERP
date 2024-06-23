using maERP.Domain.Entities;
using maERP.SalesChannels.Models;

namespace maERP.SalesChannels.Contracts;

public interface IOrderImportRepository
{
    Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportOrder importOrder);
}