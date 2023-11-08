using maERP.Server.Models;
using maERP.Shared.Dtos.SalesChannel;

namespace maERP.Server.Repository;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{
    Task<SalesChannelDetailDto> GetDetails(int id);
    Task<SalesChannelDetailDto> AddWithDetailsAsync(SalesChannelCreateDto salesChannelCreateDto);
    Task UpdateWithDetailsAsync(int id, SalesChannelUpdateDto salesChannelUpdateDto);
}