using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticOrder
{
    [Inject]
    public required IStatisticService StatisticService { get; set; }

    private StatisticOrderResponse? _statisticOrder;

    protected override async Task OnInitializedAsync()
    {
        _statisticOrder = await StatisticService.GetStatisticOrderAsync();
    }
}