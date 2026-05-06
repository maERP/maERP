using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Persistence;

public interface ISalesRepository : IGenericRepository<Sales>
{
    Task<Sales?> GetWithDetailsAsync(Guid id);
    Task<Sales?> GetByRemoteSalesIdAsync(Guid salesChannelId, string remoteSalesId);
    Task<List<SalesHistory>> GetSalesHistoryAsync(Guid salesId);
    Task<bool> CanCreateInvoice(Guid salesId);
    Task<int> GetNextSalesIdAsync();
}