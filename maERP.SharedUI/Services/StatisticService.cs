using AutoMapper;
using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class StatisticService : BaseHttpService, IStatisticService
{
    private readonly IMapper _mapper;

    public StatisticService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<StatisticOrderResponse> GetStatisticOrderAsync()
    {
        await AddBearerToken();
        return await _client.OrderStatisticAsync();
    }
}