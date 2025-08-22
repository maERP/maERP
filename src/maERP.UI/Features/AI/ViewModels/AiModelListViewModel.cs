using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiModel;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.AI.ViewModels;

public partial class AiModelListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<AiModelListDto> aiModels = new();

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
    private AiModelListDto? selectedAiModel;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Task>? NavigateToAiModelInput { get; set; }
    public Action<int>? NavigateToAiModelDetail { get; set; }

    public AiModelListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadAiModelsAsync();
    }

    [RelayCommand]
    private async Task LoadAiModelsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<AiModelListDto>("aimodels", CurrentPage, PageSize, SearchText, "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                AiModels.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                AiModels.Clear();
                foreach (var aiModel in result.Data)
                {
                    AiModels.Add(aiModel);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {AiModels.Count} AI models successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der AI-Modelle";
                AiModels.Clear();
                _debugService.LogError($"Failed to load AI models: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der AI-Modelle: {ex.Message}";
            AiModels.Clear();
            _debugService.LogError($"Exception loading AI models: {ex}");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("LoadAiModelsAsync completed");
        }
    }

    [RelayCommand]
    private async Task SearchAiModelsAsync()
    {
        CurrentPage = 0;
        await LoadAiModelsAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadAiModelsAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadAiModelsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadAiModelsAsync();
        }
    }

    [RelayCommand]
    private void SelectAiModel(AiModelListDto? aiModel)
    {
        SelectedAiModel = aiModel;
    }

    [RelayCommand]
    private async Task AddAiModel()
    {
        if (NavigateToAiModelInput != null)
            await NavigateToAiModelInput();
    }

    [RelayCommand]
    private void OpenAiModelDetails(AiModelListDto? aiModel)
    {
        if (aiModel == null || NavigateToAiModelDetail == null) return;

        SelectedAiModel = aiModel;
        NavigateToAiModelDetail(aiModel.Id);
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}