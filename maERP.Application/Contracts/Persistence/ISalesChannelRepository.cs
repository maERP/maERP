using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface ISalesChannelRepository : IGenericRepository<Domain.Models.SalesChannel>
{
    Task<Domain.Models.SalesChannel> GetDetails(int id);
    // Task<SalesChannel> AddWithDetailsAsync(SalesChannel salesChannelCreateDto);
    // Task UpdateWithDetailsAsync(int id, SalesChannel salesChannelUpdateDto);
}