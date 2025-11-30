using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.AiModels.Services;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.AiModels.Models;

/// <summary>
/// Data record for passing AI model ID to the edit page.
/// </summary>
public record AiModelEditData(Guid? aiModelId);

/// <summary>
/// Model for AI model edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class AiModelEditModel : AsyncInitializableModel
{
    private readonly IAiModelService _aiModelService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _aiModelId;

    // Model fields
    private string _name = string.Empty;
    private AiModelType _aiModelType = AiModelType.None;
    private string _apiUrl = string.Empty;
    private string _apiUsername = string.Empty;
    private string _apiPassword = string.Empty;
    private string _apiKey = string.Empty;
    private uint _nCtx;

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public AiModelEditModel(
        IAiModelService aiModelService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<AiModelEditModel> logger,
        AiModelEditData? data = null)
        : base(logger)
    {
        _aiModelService = aiModelService;
        _navigator = navigator;
        _localizer = localizer;
        _aiModelId = data?.aiModelId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        if (_aiModelId.HasValue)
        {
            await LoadAiModelAsync(ct);
        }
    }

    public bool IsEditMode => _aiModelId.HasValue;

    public string Title => IsEditMode
        ? _localizer["AiModelEditPage.TitleEdit"]
        : _localizer["AiModelEditPage.TitleNew"];

    /// <summary>
    /// Available AI model type options for the ComboBox.
    /// </summary>
    public IReadOnlyList<AiModelTypeOption> AiModelTypeOptions { get; } = new List<AiModelTypeOption>
    {
        new(AiModelType.None, "AiModelType.None"),
        new(AiModelType.Ollama, "AiModelType.Ollama"),
        new(AiModelType.VLlm, "AiModelType.VLlm"),
        new(AiModelType.LmStudio, "AiModelType.LmStudio"),
        new(AiModelType.ChatGpt4O, "AiModelType.ChatGpt4O"),
        new(AiModelType.Claude35, "AiModelType.Claude35")
    };

    #region Model Fields

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public AiModelType AiModelType
    {
        get => _aiModelType;
        set => SetProperty(ref _aiModelType, value);
    }

    public string ApiUrl
    {
        get => _apiUrl;
        set => SetProperty(ref _apiUrl, value);
    }

    public string ApiUsername
    {
        get => _apiUsername;
        set => SetProperty(ref _apiUsername, value);
    }

    public string ApiPassword
    {
        get => _apiPassword;
        set => SetProperty(ref _apiPassword, value);
    }

    public string ApiKey
    {
        get => _apiKey;
        set => SetProperty(ref _apiKey, value);
    }

    public uint NCtx
    {
        get => _nCtx;
        set => SetProperty(ref _nCtx, value);
    }

    /// <summary>
    /// NCtx as string for TextBox binding (uint doesn't bind well to TextBox).
    /// </summary>
    public string NCtxString
    {
        get => _nCtx.ToString();
        set
        {
            if (uint.TryParse(value, out var parsed))
            {
                NCtx = parsed;
            }
        }
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
        !string.IsNullOrWhiteSpace(Name) &&
        !IsLoading;

    #endregion

    private async Task LoadAiModelAsync(CancellationToken ct)
    {
        if (!_aiModelId.HasValue) return;

        var aiModel = await _aiModelService.GetAiModelAsync(_aiModelId.Value, ct);
        if (aiModel != null)
        {
            Name = aiModel.Name;
            AiModelType = aiModel.AiModelType;
            ApiUsername = aiModel.ApiUsername ?? string.Empty;
            ApiPassword = aiModel.ApiPassword ?? string.Empty;
            ApiKey = aiModel.ApiKey ?? string.Empty;
            NCtx = aiModel.NCtx;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new AiModelInputDto
            {
                Name = Name,
                AiModelType = AiModelType,
                ApiUrl = ApiUrl,
                ApiUsername = ApiUsername,
                ApiPassword = ApiPassword,
                ApiKey = ApiKey,
                NCtx = NCtx
            };

            if (_aiModelId.HasValue)
            {
                input.Id = _aiModelId.Value;
                await _aiModelService.UpdateAiModelAsync(_aiModelId.Value, input, ct);
            }
            else
            {
                await _aiModelService.CreateAiModelAsync(input, ct);
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
            ErrorMessage = string.Format(_localizer["AiModelEditPage.Error.SaveFailed"], ex.Message);
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
        if (propertyName is nameof(Name))
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

/// <summary>
/// Represents an AI model type option for the ComboBox.
/// </summary>
public record AiModelTypeOption(AiModelType Value, string ResourceKey);
