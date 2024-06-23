using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IStatisticService
{
    Task<StatisticOrderResponse> GetStatisticOrderAsync();
}
