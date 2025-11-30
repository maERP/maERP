using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Core.Models;
using maERP.Client.Features.AiModels.Services;
using maERP.Client.Features.AiPrompts.Services;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Dtos.AiPrompt;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.AiPrompts.Models;

/// <summary>
/// Data record for passing AI prompt ID to the edit page.
/// </summary>
public record AiPromptEditData(Guid? aiPromptId);

/// <summary>
/// Model for AI prompt edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class AiPromptEditModel : AsyncInitializableModel
{
    private readonly IAiPromptService _aiPromptService;
    private readonly IAiModelService _aiModelService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _aiPromptId;

    // Model fields
    private Guid _aiModelId;
    private string _identifier = string.Empty;
    private string _promptText = string.Empty;

    // AI Models for dropdown
    private ObservableCollection<AiModelListDto> _aiModels = new();

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public AiPromptEditModel(
        IAiPromptService aiPromptService,
        IAiModelService aiModelService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<AiPromptEditModel> logger,
        AiPromptEditData? data = null)
        : base(logger)
    {
        _aiPromptService = aiPromptService;
        _aiModelService = aiModelService;
        _navigator = navigator;
        _localizer = localizer;
        _aiPromptId = data?.aiPromptId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        // Load AI models for dropdown
        await LoadAiModelsAsync(ct);

        if (_aiPromptId.HasValue)
        {
            await LoadAiPromptAsync(ct);
        }
    }

    private async Task LoadAiModelsAsync(CancellationToken ct)
    {
        var parameters = new QueryParameters
        {
            PageNumber = 0,
            PageSize = 1000,
            OrderBy = "Name Ascending"
        };

        var response = await _aiModelService.GetAiModelsAsync(parameters, ct);
        AiModels.Clear();
        foreach (var aiModel in response.Data)
        {
            AiModels.Add(aiModel);
        }

        // Set default AI model if creating new and models are available
        if (!_aiPromptId.HasValue && AiModels.Count > 0)
        {
            AiModelId = AiModels[0].Id;
        }
    }

    public bool IsEditMode => _aiPromptId.HasValue;

    public string Title => IsEditMode
        ? _localizer["AiPromptEditPage.TitleEdit"]
        : _localizer["AiPromptEditPage.TitleNew"];

    #region Model Fields

    public Guid AiModelId
    {
        get => _aiModelId;
        set => SetProperty(ref _aiModelId, value);
    }

    public string Identifier
    {
        get => _identifier;
        set => SetProperty(ref _identifier, value);
    }

    public string PromptText
    {
        get => _promptText;
        set => SetProperty(ref _promptText, value);
    }

    /// <summary>
    /// Available AI models for the ComboBox.
    /// </summary>
    public ObservableCollection<AiModelListDto> AiModels
    {
        get => _aiModels;
        set => SetProperty(ref _aiModels, value);
    }

    #endregion

    #region UI State

    /// <summary>
    /// Indicates whether a save operation is in progress.
    /// </summary>
    public bool IsSaving
    {
        get => _isSaving;
        private set
        {
            if (SetProperty(ref _isSaving, value))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    /// <summary>
    /// Combined loading state (initializing or saving).
    /// </summary>
    public bool IsLoading => IsInitializing || IsSaving;

    /// <summary>
    /// Inverse of IsLoading for binding convenience.
    /// </summary>
    public bool IsNotLoading => !IsLoading;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool CanSave =>
        !string.IsNullOrWhiteSpace(Identifier) &&
        !string.IsNullOrWhiteSpace(PromptText) &&
        AiModelId != Guid.Empty &&
        !IsLoading;

    #endregion

    private async Task LoadAiPromptAsync(CancellationToken ct)
    {
        if (!_aiPromptId.HasValue) return;

        var aiPrompt = await _aiPromptService.GetAiPromptAsync(_aiPromptId.Value, ct);
        if (aiPrompt != null)
        {
            AiModelId = aiPrompt.AiModelId;
            Identifier = aiPrompt.Identifier;
            PromptText = aiPrompt.PromptText;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new AiPromptInputDto
            {
                AiModelId = AiModelId,
                Identifier = Identifier,
                PromptText = PromptText
            };

            if (_aiPromptId.HasValue)
            {
                input.Id = _aiPromptId.Value;
                await _aiPromptService.UpdateAiPromptAsync(_aiPromptId.Value, input, ct);
            }
            else
            {
                await _aiPromptService.CreateAiPromptAsync(input, ct);
            }

            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["AiPromptEditPage.Error.SaveFailed"], ex.Message);
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task CancelAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        // Update dependent properties
        if (propertyName is nameof(Identifier) or nameof(PromptText) or nameof(AiModelId))
        {
            base.OnPropertyChanged(nameof(CanSave));
        }

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}
