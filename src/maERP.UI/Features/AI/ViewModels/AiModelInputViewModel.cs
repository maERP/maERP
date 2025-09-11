using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.AI.ViewModels;

public partial class AiModelInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid aiModelId;

    [ObservableProperty]
    [Required(ErrorMessage = "Name ist erforderlich")]
    [NotifyDataErrorInfo]
    private string name = string.Empty;

    [ObservableProperty]
    private AiModelType aiModelType = AiModelType.None;

    [ObservableProperty]
    private string apiUsername = string.Empty;

    [ObservableProperty]
    private string apiPassword = string.Empty;

    [ObservableProperty]
    private string apiKey = string.Empty;

    [ObservableProperty]
    private uint nCtx;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => AiModelId != Guid.Empty;
    public string PageTitle => IsEditMode ? "AI-Modell bearbeiten" : "Neues AI-Modell";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToAiModelDetail { get; set; }

    public AiModelInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(Guid aiModelId = default)
    {
        AiModelId = aiModelId;

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
    private async Task LoadAsync()
    {
        if (AiModelId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<AiModelDetailDto>($"aimodels/{AiModelId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var aiModel = result.Data;
                Name = aiModel.Name;
                AiModelType = aiModel.AiModelType;
                ApiUsername = aiModel.ApiUsername;
                ApiPassword = aiModel.ApiPassword;
                ApiKey = aiModel.ApiKey;
                NCtx = aiModel.NCtx;
                _debugService.LogInfo($"Loaded AI model {AiModelId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des AI-Modells {AiModelId}";
                _debugService.LogError($"Failed to load AI model {AiModelId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des AI-Modells: {ex.Message}";
            _debugService.LogError($"Exception loading AI model {AiModelId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!ValidateForm()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var inputDto = new AiModelInputDto
            {
                Id = AiModelId,
                Name = Name,
                AiModelType = AiModelType,
                ApiUsername = ApiUsername,
                ApiPassword = ApiPassword,
                ApiKey = ApiKey,
                NCtx = NCtx
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<AiModelInputDto, Guid>($"aimodels/{AiModelId}", inputDto)
                : await _httpService.PostAsync<AiModelInputDto, Guid>("aimodels", inputDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"AI model {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToAiModelDetail != null)
                {
                    await NavigateToAiModelDetail(result.Data);
                }
                else
                {
                    GoBackAction?.Invoke();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Aktualisieren" : "Erstellen")} des AI-Modells";
                _debugService.LogError($"Failed to save AI model: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des AI-Modells: {ex.Message}";
            _debugService.LogError($"Exception saving AI model: {ex}");
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

    [RelayCommand]
    private void ClearForm()
    {
        Name = string.Empty;
        AiModelType = AiModelType.None;
        ApiUsername = string.Empty;
        ApiPassword = string.Empty;
        ApiKey = string.Empty;
        NCtx = 0;
        ErrorMessage = string.Empty;
        ClearErrors();
    }

    private bool ValidateForm()
    {
        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Validierungsfehler.";
            return false;
        }
        return true;
    }
}