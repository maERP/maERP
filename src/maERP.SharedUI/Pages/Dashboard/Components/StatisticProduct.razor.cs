using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticProduct
{
    [Inject]
    public required IStatisticService _statisticService { get; set; }

    private StatisticProductResponse? statisticProduct;

    protected override async Task OnInitializedAsync()
    {
        statisticProduct = await _statisticService.GetStatisticProductAsync();
    }
}