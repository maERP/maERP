using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class SalesChannelService : BaseHttpService, ISalesChannelService
{
    private readonly IMapper _mapper;

    public SalesChannelService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<SalesChannelVm>> GetSalesChannels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var salesChannels = await Client.SalesChannelsGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<SalesChannelVm>>(salesChannels);
    }

    public async Task<SalesChannelVm> GetSalesChannelDetails(int id)
    {
        await AddBearerToken();
        var salesChannel = await Client.SalesChannelsGET2Async(id);
        return _mapper.Map<SalesChannelVm>(salesChannel);
    }

    public async Task<Response<Guid>> CreateSalesChannel(SalesChannelVm salesChannel)
    {
        try
        {
            await AddBearerToken();
            var salesChannelCreateCommand = _mapper.Map<SalesChannelCreateCommand>(salesChannel);
            await Client.SalesChannelsPOSTAsync(salesChannelCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateSalesChannel(int id, SalesChannelVm salesChannel)
    {
        try
        {
            await AddBearerToken();
            var salesChannelUpdateCommand = _mapper.Map<SalesChannelUpdateCommand>(salesChannel);
            await Client.SalesChannelsPUTAsync(id, salesChannelUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteSalesChannel(int id)
    {
        try
        {
            await AddBearerToken();
            await Client.SalesChannelsDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}