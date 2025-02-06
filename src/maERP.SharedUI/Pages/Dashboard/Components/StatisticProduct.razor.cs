using maERP.Domain.Dtos.Statistic;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticProduct
{
    [Inject]
    public required IHttpService HttpService { get; set; }

    private StatisticProductDto? _statisticProduct;

    protected override async Task OnInitializedAsync()
    {
        _statisticProduct = await HttpService.GetAsync<StatisticProductDto>("/api/v1/StatisticProduct") ??
                           new StatisticProductDto();
    }
}