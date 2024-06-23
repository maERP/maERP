using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{
    Task<SalesChannel> GetDetails(int id);
    Task<bool> SalesChannelIsUniqueAsync(SalesChannel salesChannel, int? id = null);
    // Task<SalesChannel> AddWithDetailsAsync(SalesChannel salesChannelCreateDto);
    // Task UpdateWithDetailsAsync(int id, SalesChannel salesChannelUpdateDto);
}