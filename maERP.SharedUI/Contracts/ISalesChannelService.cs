using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ISalesChannelService
{
    Task<List<SalesChannelVM>> GetSalesChannels();
    Task<SalesChannelVM> GetSalesChannelDetails(int id);
    Task<Response<Guid>> CreateSalesChannel(SalesChannelVM salesChannel);
    Task<Response<Guid>> UpdateSalesChannel(int id, SalesChannelVM salesChannel);
    Task<Response<Guid>> DeleteSalesChannel(int id);
}