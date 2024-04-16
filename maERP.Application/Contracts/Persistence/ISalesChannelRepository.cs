using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{
    Task<SalesChannel> GetDetails(int id);
    // Task<SalesChannel> AddWithDetailsAsync(SalesChannel salesChannelCreateDto);
    // Task UpdateWithDetailsAsync(int id, SalesChannel salesChannelUpdateDto);
}