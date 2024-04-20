using AutoMapper;
using Blazored.LocalStorage;
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

    public async Task<List<SalesChannelVM>> GetSalesChannels()
    {
        await AddBearerToken();
        var salesChannels = await _client.SalesChannelsAllAsync();
        return _mapper.Map<List<SalesChannelVM>>(salesChannels);
    }

    public async Task<SalesChannelVM> GetSalesChannelDetails(int id)
    {
        await AddBearerToken();
        var salesChannel = await _client.SalesChannelsGETAsync(id);
        return _mapper.Map<SalesChannelVM>(salesChannel);
    }

    public async Task<Response<Guid>> CreateSalesChannel(SalesChannelVM salesChannel)
    {
        try
        {
            await AddBearerToken();
            var createSalesChannelCommand = _mapper.Map<CreateSalesChannelCommand>(salesChannel);
            await _client.SalesChannelsPOSTAsync(createSalesChannelCommand);
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

    public async Task<Response<Guid>> UpdateSalesChannel(int id, SalesChannelVM salesChannel)
    {
        try
        {
            await AddBearerToken();
            var updateSalesChannelCommand = _mapper.Map<UpdateSalesChannelCommand>(salesChannel);
            await _client.SalesChannelsPUTAsync(id.ToString(), updateSalesChannelCommand);
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
            await _client.SalesChannelsDELETEAsync(id);
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