using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.GoodsReceipts.ViewModels;

public partial class GoodsReceiptListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<GoodsReceiptListDto> goodsReceipts = new();

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
    private GoodsReceiptListDto? selectedGoodsReceipt;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Task>? NavigateToCreateGoodsReceipt { get; set; }

    public GoodsReceiptListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadGoodsReceiptsAsync();
    }

    [RelayCommand]
    private async Task LoadGoodsReceiptsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<GoodsReceiptListDto>("goodsreceipts", CurrentPage, PageSize, SearchText, "ReceiptDate Descending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                GoodsReceipts.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                GoodsReceipts.Clear();
                foreach (var goodsReceipt in result.Data)
                {
                    GoodsReceipts.Add(goodsReceipt);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {GoodsReceipts.Count} goods receipts successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Wareneingänge";
                GoodsReceipts.Clear();
                _debugService.LogError($"Failed to load goods receipts: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Wareneingänge: {ex.Message}";
            GoodsReceipts.Clear();
            _debugService.LogError(ex, "Exception loading goods receipts");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SearchGoodsReceiptsAsync()
    {
        CurrentPage = 0;
        await LoadGoodsReceiptsAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadGoodsReceiptsAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadGoodsReceiptsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadGoodsReceiptsAsync();
        }
    }

    [RelayCommand]
    private void SelectGoodsReceipt(GoodsReceiptListDto? receipt)
    {
        SelectedGoodsReceipt = receipt;
    }

    [RelayCommand]
    private async Task CreateNewGoodsReceipt()
    {
        if (NavigateToCreateGoodsReceipt == null) return;

        await NavigateToCreateGoodsReceipt();
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}