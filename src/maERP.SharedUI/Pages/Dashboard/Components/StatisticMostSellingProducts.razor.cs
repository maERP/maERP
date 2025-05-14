using maERP.Domain.Dtos.Statistic;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticMostSellingProducts
{
    [Inject]
    public required IHttpService HttpService { get; set; }

    private StatisticMostSellingProductsDto? _statisticMostSellingProducts;

    protected override async Task OnInitializedAsync()
    {
        _statisticMostSellingProducts = await HttpService.GetAsync<StatisticMostSellingProductsDto>("/api/v1/Statistics/MostSellingProducts") ??
                           new StatisticMostSellingProductsDto();
    }
}