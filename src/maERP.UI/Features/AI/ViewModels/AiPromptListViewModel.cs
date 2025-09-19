using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiPrompt;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.AI.ViewModels;

public partial class AiPromptListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<AiPromptListDto> aiPrompts = new();

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
    private AiPromptListDto? selectedAiPrompt;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Task>? NavigateToCreateAiPrompt { get; set; }
    public Func<Guid, Task>? NavigateToAiPromptDetail { get; set; }

    public AiPromptListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadAiPromptsAsync();
    }

    [RelayCommand]
    private async Task LoadAiPromptsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<AiPromptListDto>("aiprompts", CurrentPage, PageSize, SearchText, "Identifier Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                AiPrompts.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                AiPrompts.Clear();
                foreach (var aiPrompt in result.Data)
                {
                    AiPrompts.Add(aiPrompt);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {AiPrompts.Count} AI prompts successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der AI-Prompts";
                AiPrompts.Clear();
                _debugService.LogError($"Failed to load AI prompts: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der AI-Prompts: {ex.Message}";
            AiPrompts.Clear();
            _debugService.LogError($"Exception loading AI prompts: {ex}");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("LoadAiPromptsAsync completed");
        }
    }

    [RelayCommand]
    private async Task SearchAiPromptsAsync()
    {
        CurrentPage = 0;
        await LoadAiPromptsAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadAiPromptsAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadAiPromptsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadAiPromptsAsync();
        }
    }

    [RelayCommand]
    private void SelectAiPrompt(AiPromptListDto? aiPrompt)
    {
        SelectedAiPrompt = aiPrompt;
    }

    [RelayCommand]
    private async Task CreateAiPrompt()
    {
        if (NavigateToCreateAiPrompt != null)
        {
            await NavigateToCreateAiPrompt();
        }
    }

    [RelayCommand]
    private async Task OpenAiPromptDetail(AiPromptListDto? aiPrompt)
    {
        if (aiPrompt != null && NavigateToAiPromptDetail != null)
        {
            await NavigateToAiPromptDetail(aiPrompt.Id);
        }
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}