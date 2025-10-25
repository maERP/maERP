using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Dtos.AiPrompt;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.AI.ViewModels;

public partial class AiPromptInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid aiPromptId;

    [ObservableProperty]
    [Required(ErrorMessage = "AI-Modell ist erforderlich")]
    [NotifyDataErrorInfo]
    private Guid aiModelId;

    [ObservableProperty]
    [Required(ErrorMessage = "Identifier ist erforderlich")]
    [StringLength(200, ErrorMessage = "Identifier darf maximal 200 Zeichen lang sein")]
    [NotifyDataErrorInfo]
    private string identifier = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Prompt-Text ist erforderlich")]
    [StringLength(5000, ErrorMessage = "Prompt-Text darf maximal 5000 Zeichen lang sein")]
    [NotifyDataErrorInfo]
    private string promptText = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    [ObservableProperty]
    private ObservableCollection<AiModelListDto> availableAiModels = new();

    [ObservableProperty]
    private AiModelListDto? selectedAiModel;

    public bool IsEditMode => AiPromptId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"AI-Prompt #{AiPromptId} bearbeiten" : "Neuen AI-Prompt erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToAiPromptDetail { get; set; }

    public AiPromptInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(Guid aiPromptId = default)
    {
        AiPromptId = aiPromptId;
        await LoadAiModelsAsync();

        if (IsEditMode)
        {
            await LoadAsync();
        }
        else
        {
            ClearForm();
        }
    }

    [RelayCommand]
    private async Task LoadAiModelsAsync()
    {
        try
        {
            var result = await _httpService.GetAsync<ObservableCollection<AiModelListDto>>("aimodels");

            if (result?.Succeeded == true && result.Data != null)
            {
                AvailableAiModels = result.Data;
                _debugService.LogInfo($"Loaded {AvailableAiModels.Count} AI models");
            }
            else
            {
                AvailableAiModels = new ObservableCollection<AiModelListDto>();
                _debugService.LogWarning("Failed to load AI models or empty result");
            }
        }
        catch (Exception ex)
        {
            AvailableAiModels = new ObservableCollection<AiModelListDto>();
            _debugService.LogError($"Exception loading AI models: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (AiPromptId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<AiPromptDetailDto>($"aiprompts/{AiPromptId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var prompt = result.Data;
                AiModelId = prompt.AiModelId;
                Identifier = prompt.Identifier;
                PromptText = prompt.PromptText;

                SelectedAiModel = AvailableAiModels.FirstOrDefault(m => m.Id == AiModelId);
                _debugService.LogInfo($"Loaded AI prompt {AiPromptId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des AI-Prompts {AiPromptId}";
                _debugService.LogError($"Failed to load AI prompt {AiPromptId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des AI-Prompts: {ex.Message}";
            _debugService.LogError($"Exception loading AI prompt {AiPromptId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsSaving) return;

        // Update AiModelId from selected model
        if (SelectedAiModel != null)
        {
            AiModelId = SelectedAiModel.Id;
        }

        if (!ValidateForm()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var dto = new AiPromptInputDto
            {
                Id = AiPromptId,
                AiModelId = AiModelId,
                Identifier = Identifier,
                PromptText = PromptText
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<AiPromptInputDto, Guid>($"aiprompts/{AiPromptId}", dto)
                : await _httpService.PostAsync<AiPromptInputDto, Guid>("aiprompts", dto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"AI prompt {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToAiPromptDetail != null)
                {
                    await NavigateToAiPromptDetail(AiPromptId);
                }
                else if (!IsEditMode && NavigateToAiPromptDetail != null)
                {
                    var createdPromptId = result.Data;
                    if (createdPromptId != Guid.Empty)
                    {
                        await NavigateToAiPromptDetail(createdPromptId);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    GoBackAction?.Invoke();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Aktualisieren" : "Erstellen")} des AI-Prompts";
                _debugService.LogError($"Failed to save AI prompt: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError($"Exception saving AI prompt: {ex}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoBackAction?.Invoke();
    }

    private bool ValidateForm()
    {
        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Validierungsfehler.";
            return false;
        }

        var errors = new List<string>();

        if (SelectedAiModel == null || AiModelId == Guid.Empty)
            errors.Add("Bitte wählen Sie ein AI-Modell aus");

        if (string.IsNullOrWhiteSpace(Identifier))
            errors.Add("Identifier ist erforderlich");

        if (string.IsNullOrWhiteSpace(PromptText))
            errors.Add("Prompt-Text ist erforderlich");

        if (errors.Any())
        {
            ErrorMessage = string.Join(", ", errors);
            return false;
        }

        return true;
    }

    private void ClearForm()
    {
        AiModelId = Guid.Empty;
        Identifier = string.Empty;
        PromptText = string.Empty;
        SelectedAiModel = null;
        ErrorMessage = string.Empty;
    }

    [RelayCommand]
    private void TestPrompt()
    {
        if (SelectedAiModel == null || string.IsNullOrWhiteSpace(PromptText))
        {
            ErrorMessage = "Bitte wählen Sie ein AI-Modell und geben Sie einen Prompt-Text ein";
            return;
        }

        _debugService.LogDebug($"Testing prompt with model {SelectedAiModel.Name}: {PromptText[..Math.Min(50, PromptText.Length)]}...");
    }

    [RelayCommand]
    private void LoadTemplate()
    {
        var templates = new[]
        {
            "Analysiere den folgenden Text und extrahiere die wichtigsten Informationen:\n\n{input_text}\n\nGib eine strukturierte Antwort zurück.",
            "Übersetze den folgenden Text ins Deutsche:\n\n{input_text}",
            "Fasse den folgenden Text in 3 Sätzen zusammen:\n\n{input_text}",
            "Korrigiere Grammatik und Rechtschreibung im folgenden Text:\n\n{input_text}"
        };

        var randomTemplate = templates[new Random().Next(templates.Length)];
        PromptText = randomTemplate;
        _debugService.LogDebug("Loaded template prompt");
    }

    [RelayCommand]
    private void FormatText()
    {
        if (string.IsNullOrWhiteSpace(PromptText)) return;

        var formatted = PromptText
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Trim();

        PromptText = formatted;
        _debugService.LogDebug("Formatted prompt text");
    }

    partial void OnSelectedAiModelChanged(AiModelListDto? value)
    {
        if (value != null)
        {
            AiModelId = value.Id;
        }
    }
}
