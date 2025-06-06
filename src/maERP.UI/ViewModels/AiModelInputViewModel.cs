using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class AiModelInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private int aiModelId;

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
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => AiModelId > 0;
    public string PageTitle => IsEditMode ? "AI-Modell bearbeiten" : "Neues AI-Modell";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToAiModelDetail { get; set; }

    public AiModelInputViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(int aiModelId = 0)
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
        if (AiModelId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<AiModelDetailDto>($"aimodels/{AiModelId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var aiModel = result.Data;
                Name = aiModel.Name;
                AiModelType = aiModel.AiModelType;
                ApiUsername = aiModel.ApiUsername;
                ApiPassword = aiModel.ApiPassword;
                ApiKey = aiModel.ApiKey;
                System.Diagnostics.Debug.WriteLine($"Loaded AI model {AiModelId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des AI-Modells {AiModelId}";
                System.Diagnostics.Debug.WriteLine($"Failed to load AI model {AiModelId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des AI-Modells: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception loading AI model {AiModelId}: {ex}");
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
                ApiKey = ApiKey
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<AiModelInputDto, int>($"aimodels/{AiModelId}", inputDto)
                : await _httpService.PostAsync<AiModelInputDto, int>("aimodels", inputDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine($"AI model {(IsEditMode ? "updated" : "created")} successfully");
                
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
                System.Diagnostics.Debug.WriteLine($"Failed to save AI model: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des AI-Modells: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception saving AI model: {ex}");
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