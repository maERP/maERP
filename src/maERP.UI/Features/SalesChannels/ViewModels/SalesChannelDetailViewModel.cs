using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Enums;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.SalesChannels.ViewModels;

public partial class SalesChannelDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private SalesChannelDetailDto? salesChannel;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int salesChannelId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && SalesChannel != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToSalesChannelInput { get; set; }

    // Computed properties for better display
    public bool HasUrl => SalesChannel != null && !string.IsNullOrEmpty(SalesChannel.Url);
    public bool HasUsername => SalesChannel != null && !string.IsNullOrEmpty(SalesChannel.Username);
    public bool HasPassword => SalesChannel != null && !string.IsNullOrEmpty(SalesChannel.Password);

    // Connection properties
    public bool HasConnectionInfo => HasUrl && HasUsername;

    // Import/Export capabilities
    public bool HasImportCapabilities => SalesChannel != null && (SalesChannel.ImportProducts || SalesChannel.ImportCustomers || SalesChannel.ImportOrders);
    public bool HasExportCapabilities => SalesChannel != null && (SalesChannel.ExportProducts || SalesChannel.ExportCustomers || SalesChannel.ExportOrders);
    public bool HasAnyCapabilities => HasImportCapabilities || HasExportCapabilities;

    // SalesChannelType display
    public string SalesChannelTypeDisplay => SalesChannel?.SalesChannelType switch
    {
        SalesChannelType.PointOfSale => "📍 Point of Sale",
        SalesChannelType.Shopware5 => "🛒 Shopware 5",
        SalesChannelType.Shopware6 => "🛒 Shopware 6",
        SalesChannelType.WooCommerce => "🛍️ WooCommerce",
        SalesChannelType.eBay => "🏪 eBay",
        _ => SalesChannel?.SalesChannelType.ToString() ?? "Unbekannt"
    };

    public string SalesChannelTypeIcon => SalesChannel?.SalesChannelType switch
    {
        SalesChannelType.PointOfSale => "📍",
        SalesChannelType.Shopware5 => "🛒",
        SalesChannelType.Shopware6 => "🛒",
        SalesChannelType.WooCommerce => "🛍️",
        SalesChannelType.eBay => "🏪",
        _ => "🔗"
    };

    // Password display (masked)
    public string PasswordDisplay => HasPassword ? "••••••••" : "Nicht gesetzt";

    public SalesChannelDetailViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int salesChannelId)
    {
        SalesChannelId = salesChannelId;
        await LoadSalesChannelAsync();
    }

    [RelayCommand]
    private async Task LoadSalesChannelAsync()
    {
        if (SalesChannelId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<SalesChannelDetailDto>($"saleschannels/{SalesChannelId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                SalesChannel = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                SalesChannel = result.Data;
                OnPropertyChanged(nameof(HasUrl));
                OnPropertyChanged(nameof(HasUsername));
                OnPropertyChanged(nameof(HasPassword));
                OnPropertyChanged(nameof(HasConnectionInfo));
                OnPropertyChanged(nameof(HasImportCapabilities));
                OnPropertyChanged(nameof(HasExportCapabilities));
                OnPropertyChanged(nameof(HasAnyCapabilities));
                OnPropertyChanged(nameof(SalesChannelTypeDisplay));
                OnPropertyChanged(nameof(SalesChannelTypeIcon));
                OnPropertyChanged(nameof(PasswordDisplay));
                _debugService.LogInfo($"Loaded sales channel {SalesChannelId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Vertriebskanals {SalesChannelId}";
                SalesChannel = null;
                _debugService.LogError($"Failed to load sales channel {SalesChannelId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Vertriebskanals: {ex.Message}";
            SalesChannel = null;
            _debugService.LogError(ex, $"Exception loading sales channel {SalesChannelId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadSalesChannelAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditSalesChannel()
    {
        if (SalesChannelId <= 0 || NavigateToSalesChannelInput == null) return;

        await NavigateToSalesChannelInput(SalesChannelId);
    }
}