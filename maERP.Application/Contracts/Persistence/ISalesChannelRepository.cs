using maERP.Shared.Models.Database;
using maERP.Shared.Dtos.SalesChannel;

namespace maERP.Application.Contracts.Persistence;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{
    Task<SalesChannelDetailDto> GetDetails(int id);
    Task<SalesChannelDetailDto> AddWithDetailsAsync(SalesChannelCreateDto salesChannelCreateDto);
    Task UpdateWithDetailsAsync(int id, SalesChannelUpdateDto salesChannelUpdateDto);
}