using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.TaxClass;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Administration.ViewModels;

public partial class TaxClassDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private TaxClassDetailDto? taxClass;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int taxClassId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && TaxClass != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditTaxClass { get; set; }

    // Computed properties for better display
    public string TaxRateFormatted => TaxClass?.TaxRate.ToString("F2") + " %" ?? "0,00 %";
    
    public string TaxRateDescription => TaxClass?.TaxRate switch
    {
        0.0 => "Steuerfreie Waren und Dienstleistungen",
        7.0 => "Deutscher ermäßigter Steuersatz",
        19.0 => "Deutscher Regelsteuersatz",
        _ when TaxClass?.TaxRate > 0 && TaxClass?.TaxRate < 7.0 => "Reduzierter Steuersatz",
        _ when TaxClass?.TaxRate > 7.0 && TaxClass?.TaxRate < 19.0 => "Mittlerer Steuersatz",
        _ when TaxClass?.TaxRate > 19.0 => "Erhöhter Steuersatz",
        _ => "Individueller Steuersatz"
    };

    public string TaxRateCategory => TaxClass?.TaxRate switch
    {
        0.0 => "🆓 Steuerfrei",
        7.0 => "📉 Ermäßigt",
        19.0 => "📊 Standard",
        _ when TaxClass?.TaxRate > 0 && TaxClass?.TaxRate < 19.0 => "📉 Reduziert",
        _ when TaxClass?.TaxRate > 19.0 => "📈 Erhöht",
        _ => "📋 Individuell"
    };

    public TaxClassDetailViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int taxClassId)
    {
        TaxClassId = taxClassId;
        await LoadTaxClassAsync();
    }

    [RelayCommand]
    private async Task LoadTaxClassAsync()
    {
        if (TaxClassId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<TaxClassDetailDto>($"taxclasses/{TaxClassId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                TaxClass = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                TaxClass = result.Data;
                OnPropertyChanged(nameof(TaxRateFormatted));
                OnPropertyChanged(nameof(TaxRateDescription));
                OnPropertyChanged(nameof(TaxRateCategory));
                _debugService.LogInfo($"Loaded tax class {TaxClassId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden der Steuerklasse {TaxClassId}";
                TaxClass = null;
                _debugService.LogError($"Failed to load tax class {TaxClassId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Steuerklasse: {ex.Message}";
            TaxClass = null;
            _debugService.LogError($"Exception loading tax class {TaxClassId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadTaxClassAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditTaxClass()
    {
        if (TaxClass == null || NavigateToEditTaxClass == null) return;
        
        await NavigateToEditTaxClass(TaxClass.Id);
    }
}