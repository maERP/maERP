using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ISalesChannelService
{
    Task<PaginatedResult<SalesChannelVm>> GetSalesChannels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<SalesChannelVm> GetSalesChannelDetails(int id);
    Task<Response<Guid>> CreateSalesChannel(SalesChannelVm salesChannel);
    Task<Response<Guid>> UpdateSalesChannel(int id, SalesChannelVm salesChannel);
    Task<Response<Guid>> DeleteSalesChannel(int id);
}