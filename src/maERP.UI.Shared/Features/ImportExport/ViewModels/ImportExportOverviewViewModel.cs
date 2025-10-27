using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Shared.Services;
using maERP.UI.Shared.Shared.ViewModels;

namespace maERP.UI.Shared.Features.ImportExport.ViewModels;

public partial class ImportExportOverviewViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ImportExportCategoryItem> categories = new();

    public ImportExportOverviewViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        InitializeCategories();
    }

    private void InitializeCategories()
    {
        Categories.Clear();

        Categories.Add(new ImportExportCategoryItem
        {
            Title = "Kunden",
            Description = "Import und Export von Kundendaten als CSV-Datei",
            Icon = "👥",
            Category = ImportExportCategory.Customers,
            IsEnabled = true
        });

        // Für zukünftige Erweiterungen bereits vorbereitet
        Categories.Add(new ImportExportCategoryItem
        {
            Title = "Produkte",
            Description = "Import und Export von Produktdaten (Bald verfügbar)",
            Icon = "📦",
            Category = ImportExportCategory.Products,
            IsEnabled = false
        });

        Categories.Add(new ImportExportCategoryItem
        {
            Title = "Bestellungen",
            Description = "Export von Bestellungsdaten (Bald verfügbar)",
            Icon = "🛒",
            Category = ImportExportCategory.Orders,
            IsEnabled = false
        });

        Categories.Add(new ImportExportCategoryItem
        {
            Title = "Rechnungen",
            Description = "Export von Rechnungsdaten (Bald verfügbar)",
            Icon = "🧾",
            Category = ImportExportCategory.Invoices,
            IsEnabled = false
        });
    }

    [RelayCommand]
    private async Task ExportAsync(ImportExportCategoryItem? categoryItem)
    {
        if (categoryItem == null || !categoryItem.IsEnabled)
            return;

        _debugService.LogInfo($"Export button clicked for category: {categoryItem.Category}");

        switch (categoryItem.Category)
        {
            case ImportExportCategory.Customers:
                _debugService.LogInfo("Starting customer export...");
                await ExportCustomersAsync();
                break;
            case ImportExportCategory.Products:
            case ImportExportCategory.Orders:
            case ImportExportCategory.Invoices:
                // Für zukünftige Implementierung
                _debugService.LogWarning($"Export for {categoryItem.Category} not yet implemented");
                break;
        }
    }

    [RelayCommand]
    private Task ImportAsync(ImportExportCategoryItem? categoryItem)
    {
        if (categoryItem == null || !categoryItem.IsEnabled)
            return Task.CompletedTask;

        switch (categoryItem.Category)
        {
            case ImportExportCategory.Customers:
                // Import-Dialog öffnen (für zukünftige Implementierung)
                break;
            case ImportExportCategory.Products:
            case ImportExportCategory.Orders:
            case ImportExportCategory.Invoices:
                // Für zukünftige Implementierung
                break;
        }

        return Task.CompletedTask;
    }

    private async Task ExportCustomersAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            _debugService.LogInfo("Starting customer export...");
            _debugService.LogDebug($"HttpService authenticated: {_httpService.IsAuthenticated}");
            _debugService.LogDebug($"HttpService server URL: {_httpService.ServerUrl}");

            // Call export API
            var result = await _httpService.DownloadFileAsync(
                "importexport/customers/export",
                $"customers_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

            _debugService.LogDebug($"Download result: Success={result.Success}, Error={result.ErrorMessage}");

            if (result.Success)
            {
                // File was successfully downloaded
                ErrorMessage = $"✅ Export erfolgreich: {result.FilePath}";
                _debugService.LogInfo($"Customer export successful: {result.FilePath}");
            }
            else
            {
                ErrorMessage = result.ErrorMessage ?? "Fehler beim Export der Kundendaten";
                _debugService.LogError($"Customer export failed: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Export: {ex.Message}";
            _debugService.LogError(ex, "Exception during customer export");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

public class ImportExportCategoryItem
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public ImportExportCategory Category { get; set; }
    public bool IsEnabled { get; set; }
}

public enum ImportExportCategory
{
    Customers,
    Products,
    Orders,
    Invoices
}