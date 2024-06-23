using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class StatisticService : BaseHttpService, IStatisticService
{
    public StatisticService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
    }

    public async Task<StatisticOrderResponse> GetStatisticOrderAsync()
    {
        await AddBearerToken();
        return await _client.OrderStatisticAsync();
    }
}