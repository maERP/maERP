using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticProduct
{
    [Inject]
    public required IStatisticService StatisticService { get; set; }

    private StatisticProductResponse? _statisticProduct;

    protected override async Task OnInitializedAsync()
    {
        _statisticProduct = await StatisticService.GetStatisticProductAsync();
    }
}