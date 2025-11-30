using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.TaxClasses.Services;
using maERP.Domain.Dtos.TaxClass;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.TaxClasses.Models;

/// <summary>
/// Model for tax class edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class TaxClassEditModel : AsyncInitializableModel
{
    private readonly ITaxClassService _taxClassService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _taxClassId;

    // Tax Class Data
    private double _taxRate;

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public TaxClassEditModel(
        ITaxClassService taxClassService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<TaxClassEditModel> logger,
        TaxClassEditData? data = null)
        : base(logger)
    {
        _taxClassService = taxClassService;
        _navigator = navigator;
        _localizer = localizer;
        _taxClassId = data?.TaxClassId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        if (_taxClassId.HasValue)
        {
            await LoadTaxClassAsync(ct);
        }
    }

    public bool IsEditMode => _taxClassId.HasValue;

    public string Title => IsEditMode
        ? _localizer["TaxClassEditPage.TitleEdit"]
        : _localizer["TaxClassEditPage.TitleNew"];

    #region Tax Class Data

    public double TaxRate
    {
        get => _taxRate;
        set
        {
            if (SetProperty(ref _taxRate, value))
            {
                OnPropertyChanged(nameof(CanSave));
                OnPropertyChanged(nameof(TaxRateDisplay));
            }
        }
    }

    /// <summary>
    /// Formatted tax rate display string with percentage sign.
    /// </summary>
    public string TaxRateDisplay => $"{TaxRate:F2} %";

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

    public bool CanSave => TaxRate >= 0 && !IsLoading;

    #endregion

    private async Task LoadTaxClassAsync(CancellationToken ct)
    {
        if (!_taxClassId.HasValue) return;

        var taxClass = await _taxClassService.GetTaxClassAsync(_taxClassId.Value, ct);
        if (taxClass != null)
        {
            TaxRate = taxClass.TaxRate;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new TaxClassInputDto
            {
                TaxRate = TaxRate
            };

            if (_taxClassId.HasValue)
            {
                input.Id = _taxClassId.Value;
                await _taxClassService.UpdateTaxClassAsync(_taxClassId.Value, input, ct);
            }
            else
            {
                await _taxClassService.CreateTaxClassAsync(input, ct);
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
            ErrorMessage = string.Format(_localizer["TaxClassEditPage.Error.SaveFailed"], ex.Message);
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

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}
