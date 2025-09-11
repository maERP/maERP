using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{
    Task<SalesChannel> GetDetails(Guid id);
    Task<bool> SalesChannelIsUniqueAsync(SalesChannel salesChannel, Guid? id = null);
    // Task<SalesChannel> AddWithDetailsAsync(SalesChannel salesChannelCreateDto);
    // Task UpdateWithDetailsAsync(int id, SalesChannel salesChannelUpdateDto);
}