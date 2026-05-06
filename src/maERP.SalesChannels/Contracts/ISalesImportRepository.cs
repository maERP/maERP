using maERP.Domain.Entities;
using maERP.SalesChannels.Models;

namespace maERP.SalesChannels.Contracts;

public interface ISalesImportRepository
{
    Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportSales importSales);
}