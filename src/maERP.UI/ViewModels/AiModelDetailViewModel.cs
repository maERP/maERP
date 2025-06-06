using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class AiModelDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    private AiModelDetailDto? aiModel;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int aiModelId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && AiModel != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditAiModel { get; set; }

    // Computed properties for better display
    public bool HasApiUsername => AiModel != null && !string.IsNullOrEmpty(AiModel.ApiUsername);
    public bool HasApiPassword => AiModel != null && !string.IsNullOrEmpty(AiModel.ApiPassword);
    public bool HasApiKey => AiModel != null && !string.IsNullOrEmpty(AiModel.ApiKey);

    public string AiModelTypeDisplayName => AiModel?.AiModelType switch
    {
        AiModelType.Ollama => "Ollama",
        AiModelType.VLlm => "vLLM",
        AiModelType.LmStudio => "LM Studio",
        AiModelType.ChatGpt4O => "ChatGPT-4o",
        AiModelType.Claude35 => "Claude 3.5",
        AiModelType.None => "Nicht definiert",
        _ => "Unbekannt"
    };

    public string SecurityNote => AiModel?.AiModelType switch
    {
        AiModelType.Ollama => "Lokales Modell - keine externen API-Calls",
        AiModelType.VLlm => "Lokales Modell - keine externen API-Calls",
        AiModelType.LmStudio => "Lokales Modell - keine externen API-Calls",
        AiModelType.ChatGpt4O => "Externe API - OpenAI",
        AiModelType.Claude35 => "Externe API - Anthropic",
        _ => "Sicherheitshinweise nicht verfügbar"
    };

    public bool IsLocalModel => AiModel?.AiModelType is AiModelType.Ollama or AiModelType.VLlm or AiModelType.LmStudio;
    public bool IsExternalModel => AiModel?.AiModelType is AiModelType.ChatGpt4O or AiModelType.Claude35;

    public AiModelDetailViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(int aiModelId)
    {
        AiModelId = aiModelId;
        await LoadAiModelAsync();
    }

    [RelayCommand]
    private async Task LoadAiModelAsync()
    {
        if (AiModelId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<AiModelDetailDto>($"aimodels/{AiModelId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                AiModel = null;
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                AiModel = result.Data;
                OnPropertyChanged(nameof(HasApiUsername));
                OnPropertyChanged(nameof(HasApiPassword));
                OnPropertyChanged(nameof(HasApiKey));
                OnPropertyChanged(nameof(AiModelTypeDisplayName));
                OnPropertyChanged(nameof(SecurityNote));
                OnPropertyChanged(nameof(IsLocalModel));
                OnPropertyChanged(nameof(IsExternalModel));
                System.Diagnostics.Debug.WriteLine($"Loaded AI model {AiModelId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des AI-Modells {AiModelId}";
                AiModel = null;
                System.Diagnostics.Debug.WriteLine($"Failed to load AI model {AiModelId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des AI-Modells: {ex.Message}";
            AiModel = null;
            System.Diagnostics.Debug.WriteLine($"Exception loading AI model {AiModelId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadAiModelAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditAiModel()
    {
        if (AiModel == null || NavigateToEditAiModel == null) return;
        
        await NavigateToEditAiModel(AiModel.Id);
    }
}