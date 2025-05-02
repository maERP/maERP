using maERP.Domain.Dtos.Statistic;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticOrder
{
    [Inject]
    public required IHttpService HttpService { get; set; }

    private StatisticOrderDto _statisticOrder = new();

    protected override async Task OnInitializedAsync()
    {
        _statisticOrder = await HttpService.GetAsync<StatisticOrderDto>("/api/v1/Statistics/OrderStatistic") ??
                         new StatisticOrderDto();
    }
}