using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.SalesChannel;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.SalesChannels.ViewModels;

public partial class SalesChannelListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<SalesChannelListDto> salesChannels = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowDataGrid))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowDataGrid))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private int totalCount;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private int pageSize = 50;

    [ObservableProperty]
    private SalesChannelListDto? selectedSalesChannel;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Guid, Task>? NavigateToSalesChannelDetail { get; set; }
    public Func<Guid, Task>? NavigateToSalesChannelInput { get; set; }

    public SalesChannelListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadSalesChannelsAsync();
    }

    [RelayCommand]
    private async Task LoadSalesChannelsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<SalesChannelListDto>("saleschannels", CurrentPage, PageSize, SearchText, "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                SalesChannels.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                SalesChannels.Clear();
                foreach (var salesChannel in result.Data)
                {
                    SalesChannels.Add(salesChannel);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {SalesChannels.Count} sales channels successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Vertriebskanäle";
                SalesChannels.Clear();
                _debugService.LogError($"Failed to load sales channels: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Vertriebskanäle: {ex.Message}";
            SalesChannels.Clear();
            _debugService.LogError(ex, "Exception loading sales channels");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("finally");
        }
    }

    [RelayCommand]
    private async Task SearchSalesChannelsAsync()
    {
        CurrentPage = 0;
        await LoadSalesChannelsAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadSalesChannelsAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadSalesChannelsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadSalesChannelsAsync();
        }
    }

    [RelayCommand]
    private void SelectSalesChannel(SalesChannelListDto? salesChannel)
    {
        SelectedSalesChannel = salesChannel;
    }

    [RelayCommand]
    private async Task ViewSalesChannelDetails(SalesChannelListDto? salesChannel)
    {
        if (salesChannel == null || NavigateToSalesChannelDetail == null) return;

        SelectedSalesChannel = salesChannel;
        await NavigateToSalesChannelDetail(salesChannel.Id);
    }

    [RelayCommand]
    private async Task CreateSalesChannel()
    {
        if (NavigateToSalesChannelInput == null) return;

        await NavigateToSalesChannelInput(Guid.Empty); // Guid.Empty indicates create new
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}