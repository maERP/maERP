using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.AiPrompt;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.AI.ViewModels;

public partial class AiPromptDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    private AiPromptDetailDto? aiPrompt;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int aiPromptId;

    [ObservableProperty]
    private bool isDeleting;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && AiPrompt != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditAiPrompt { get; set; }

    public bool HasIdentifier => AiPrompt != null && !string.IsNullOrEmpty(AiPrompt.Identifier);
    public bool HasPromptText => AiPrompt != null && !string.IsNullOrEmpty(AiPrompt.PromptText);

    public AiPromptDetailViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(int aiPromptId)
    {
        AiPromptId = aiPromptId;
        await LoadAiPromptAsync();
    }

    [RelayCommand]
    private async Task LoadAiPromptAsync()
    {
        if (AiPromptId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<AiPromptDetailDto>($"aiprompts/{AiPromptId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                AiPrompt = null;
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                AiPrompt = result.Data;
                OnPropertyChanged(nameof(HasIdentifier));
                OnPropertyChanged(nameof(HasPromptText));
                System.Diagnostics.Debug.WriteLine($"Loaded AI prompt {AiPromptId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des AI-Prompts {AiPromptId}";
                AiPrompt = null;
                System.Diagnostics.Debug.WriteLine($"Failed to load AI prompt {AiPromptId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des AI-Prompts: {ex.Message}";
            AiPrompt = null;
            System.Diagnostics.Debug.WriteLine($"Exception loading AI prompt {AiPromptId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadAiPromptAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditAiPrompt()
    {
        if (AiPrompt == null || NavigateToEditAiPrompt == null) return;
        
        await NavigateToEditAiPrompt(AiPrompt.Id);
    }

    [RelayCommand]
    private void CopyPromptText()
    {
        if (AiPrompt == null || string.IsNullOrEmpty(AiPrompt.PromptText)) return;

        System.Diagnostics.Debug.WriteLine($"Copy prompt text requested for prompt {AiPrompt.Id}");
    }

    [RelayCommand]
    private void TestPrompt()
    {
        if (AiPrompt == null) return;
        
        System.Diagnostics.Debug.WriteLine($"Testing prompt {AiPrompt.Id} with model {AiPrompt.AiModelId}");
    }

    [RelayCommand]
    private async Task DeleteAiPrompt()
    {
        if (AiPrompt == null || IsDeleting) return;

        // Show confirmation dialog (simplified version)
        var confirmed = await ShowDeleteConfirmationAsync();
        if (!confirmed) return;

        IsDeleting = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.DeleteAsync($"aiprompts/{AiPrompt.Id}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("DeleteAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine($"Successfully deleted AI prompt {AiPrompt.Id}");
                GoBackAction?.Invoke();
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Löschen des AI-Prompts";
                System.Diagnostics.Debug.WriteLine($"Failed to delete AI prompt {AiPrompt.Id}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Löschen des AI-Prompts: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception deleting AI prompt {AiPrompt.Id}: {ex}");
        }
        finally
        {
            IsDeleting = false;
        }
    }

    private async Task<bool> ShowDeleteConfirmationAsync()
    {
        // Simple confirmation - in production you might want to use a proper dialog service
        try
        {
            if (AiPrompt == null) return false;
            
            // For now we'll use debug output to simulate confirmation
            // In a real implementation, you'd show a confirmation dialog
            System.Diagnostics.Debug.WriteLine($"Confirming deletion of AI prompt: {AiPrompt.Identifier}");
            
            // Simulate user confirming deletion
            await Task.Delay(100);
            return true;
        }
        catch
        {
            return false;
        }
    }
}