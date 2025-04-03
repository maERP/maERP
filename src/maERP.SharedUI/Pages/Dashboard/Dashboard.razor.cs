using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Dashboard;

public partial class Dashboard
{
    [Inject]
    public required NavigationManager? NavigationManager { get; set; }
    
    [Inject]
    public required IHttpService HttpService { get; set; }

    public ChartOptions Options = new ChartOptions();

    public List<ChartSeries> Series = new List<ChartSeries>();

    public string[] XAxisLabels = Array.Empty<string>();
    
    private bool _isLoading = true;
    private string _errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadChartDataAsync();
    }
    
    private async Task LoadChartDataAsync()
    {
        try
        {
            _isLoading = true;
            
            // Verwende das konkrete DTO statt dynamischen Typ
            var result = await HttpService.GetAsync<Result<StatisticOrderDto>>("/api/v1/Statistic/OrderStatistic");
            
            if (result != null && result.Succeeded)
            {
                var orderStatistics = result.Data;
                
                // Verwende die typisierte DailyStatistics-Liste
                if (orderStatistics.DailyStatistics.Any())
                {
                    // Sortiere die Daten nach Datum
                    var orderedDailyStats = orderStatistics.DailyStatistics.OrderBy(x => x.Date).ToList();
                    
                    // Daten für das Diagramm vorbereiten
                    var ordersData = orderedDailyStats.Select(x => (double)x.OrderCount).ToArray();
                    var customersData = orderedDailyStats.Select(x => (double)x.NewCustomerCount).ToArray();
                    
                    // Formatieren der Datumsangaben für die X-Achse
                    XAxisLabels = orderedDailyStats.Select(x => x.Date.ToString("dd.MM.")).ToArray();
                    
                    // Serien erstellen
                    Series = new List<ChartSeries>
                    {
                        new ChartSeries { Name = "neue Bestellungen", Data = ordersData },
                        new ChartSeries { Name = "neue Kunden", Data = customersData }
                    };
                }
                else
                {
                    // Fallback für leere Daten
                    XAxisLabels = new[] { "Keine Daten" };
                    Series = new List<ChartSeries>
                    {
                        new ChartSeries { Name = "neue Bestellungen", Data = new double[] { 0 } },
                        new ChartSeries { Name = "neue Kunden", Data = new double[] { 0 } }
                    };
                }
            }
            else
            {
                _errorMessage = result?.Messages.FirstOrDefault() ?? "Fehler beim Laden der Diagrammdaten";
                // Fallback für Fehlerfall
                XAxisLabels = new[] { "Fehler" };
                Series = new List<ChartSeries>
                {
                    new ChartSeries { Name = "neue Bestellungen", Data = new double[] { 0 } },
                    new ChartSeries { Name = "neue Kunden", Data = new double[] { 0 } }
                };
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"Fehler: {ex.Message}";
            // Fallback für Exception
            XAxisLabels = new[] { "Fehler" };
            Series = new List<ChartSeries>
            {
                new ChartSeries { Name = "neue Bestellungen", Data = new double[] { 0 } },
                new ChartSeries { Name = "neue Kunden", Data = new double[] { 0 } }
            };
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }
}