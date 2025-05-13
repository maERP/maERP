using maERP.Domain.Dtos.Statistic;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticSales
{
    [Inject]
    public required IHttpService HttpService { get; set; }

    private StatisticSalesDto _statisticSales = new();

    protected override async Task OnInitializedAsync()
    {
        _statisticSales = await HttpService.GetAsync<StatisticSalesDto>("/api/v1/Statistics/SalesStatistic") ??
                          new StatisticSalesDto();
    }
}