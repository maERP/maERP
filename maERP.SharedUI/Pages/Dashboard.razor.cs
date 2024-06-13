using maERP.SharedUI.Contracts;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages;

public partial class Dashboard
{
    [Inject]
    public required NavigationManager? _navigationManager { get; set; }

    [Inject]
    public required IStatisticService _statisticService { get; set; }

    private StatisticOrderResponse? statisticOrder;

    public ChartOptions Options = new ChartOptions();

    public List<ChartSeries> Series = new List<ChartSeries>
    {
        new ChartSeries() { Name = "neue Bestellungen", Data = new double[] { 90, 79, 72, 69, 62, 62, 55, 65, 70 } },
        new ChartSeries() { Name = "neue Kunden", Data = new double[] { 10, 41, 35, 51, 49, 62, 69, 91, 148 } },
    };

    public string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };

    protected override async Task OnInitializedAsync()
    {
        statisticOrder = await _statisticService.GetStatisticOrderAsync();
    }
}