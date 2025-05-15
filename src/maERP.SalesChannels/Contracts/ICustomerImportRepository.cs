using maERP.Domain.Entities;
using maERP.SalesChannels.Models;

namespace maERP.SalesChannels.Contracts;

public interface ICustomerImportRepository
{
    Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportCustomer importCustomer);
} 