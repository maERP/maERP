using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiModel;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class AiModelListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

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

    public Action<int>? NavigateToAiModelDetail { get; set; }

    public AiModelListViewModel(IHttpService httpService)
    {
        _httpService = httpService;
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
                System.Diagnostics.Debug.WriteLine("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                AiModels.Clear();
                foreach (var aiModel in result.Data)
                {
                    AiModels.Add(aiModel);
                }
                TotalCount = result.TotalCount;
                System.Diagnostics.Debug.WriteLine($"Loaded {AiModels.Count} AI models successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der AI-Modelle";
                AiModels.Clear();
                System.Diagnostics.Debug.WriteLine($"Failed to load AI models: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der AI-Modelle: {ex.Message}";
            AiModels.Clear();
            System.Diagnostics.Debug.WriteLine($"Exception loading AI models: {ex}");
        }
        finally
        {
            IsLoading = false;
            System.Diagnostics.Debug.WriteLine($"finally");
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