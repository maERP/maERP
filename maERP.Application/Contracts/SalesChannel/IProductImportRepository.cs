using maERP.Domain.Models;
using maERP.Domain.Models.SalesChannelData;

namespace maERP.Application.Contracts.SalesChannel;

public interface IProductImportRepository
{
    Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportProduct importProduct);
}
