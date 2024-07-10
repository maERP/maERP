using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Dashboard.Components;

public partial class StatisticOrder
{
    [Inject]
    public required IStatisticService _statisticService { get; set; }

    private StatisticOrderResponse? statisticOrder;

    protected override async Task OnInitializedAsync()
    {
        statisticOrder = await _statisticService.GetStatisticOrderAsync();
    }
}