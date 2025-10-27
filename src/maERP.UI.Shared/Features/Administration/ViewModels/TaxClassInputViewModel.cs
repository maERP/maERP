using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Interfaces;
using maERP.UI.Shared.Features.Administration.Validators;
using maERP.UI.Shared.Services;
using maERP.UI.Shared.Shared.Validation;

namespace maERP.UI.Shared.Features.Administration.ViewModels;

public partial class TaxClassInputViewModel : FluentValidationViewModelBase, ITaxClassInputModel
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly TaxClassClientValidator _validator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid taxClassId;

    [ObservableProperty]
    private double taxRate = 0.0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => TaxClassId != Guid.Empty;
    public string PageTitle => IsEditMode ? "Steuerklasse bearbeiten" : "Neue Steuerklasse";
    public bool ShouldShowContent => !IsLoading && !IsSaving;

    // Validation Error Properties for XAML Binding
    public string? TaxRateError => GetFirstErrorMessage(nameof(TaxRate));

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToTaxClassDetail { get; set; }

    public TaxClassInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _validator = new TaxClassClientValidator();
    }

    /// <summary>
    /// Gibt den FluentValidator für dieses ViewModel zurück.
    /// </summary>
    protected override IValidator GetValidator() => _validator;

    // Property-Change Validierung für Echtzeit-Feedback
    partial void OnTaxRateChanged(double value) => ValidateProperty(nameof(TaxRate));

    public async Task InitializeAsync(Guid taxClassId = default)
    {
        TaxClassId = taxClassId;

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
        if (TaxClassId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<TaxClassDetailDto>($"taxclasses/{TaxClassId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var taxClass = result.Data;
                TaxRate = taxClass.TaxRate;
                _debugService.LogInfo($"Loaded tax class {TaxClassId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden der Steuerklasse {TaxClassId}";
                _debugService.LogError($"Failed to load tax class {TaxClassId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Steuerklasse: {ex.Message}";
            _debugService.LogError($"Exception loading tax class {TaxClassId}: {ex}");
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
            var inputDto = new TaxClassInputDto
            {
                Id = TaxClassId,
                TaxRate = TaxRate
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<TaxClassInputDto, Guid>($"taxclasses/{TaxClassId}", inputDto)
                : await _httpService.PostAsync<TaxClassInputDto, Guid>("taxclasses", inputDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Tax class {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToTaxClassDetail != null)
                {
                    await NavigateToTaxClassDetail(result.Data);
                }
                else
                {
                    GoBackAction?.Invoke();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Aktualisieren" : "Erstellen")} der Steuerklasse";
                _debugService.LogError($"Failed to save tax class: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern der Steuerklasse: {ex.Message}";
            _debugService.LogError($"Exception saving tax class: {ex}");
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
        TaxRate = 0.0;
        ErrorMessage = string.Empty;
        ClearErrors();
    }

    private bool ValidateForm()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            ErrorMessage = "⚠️ Bitte korrigieren Sie die markierten Fehler im Formular.";
            return false;
        }

        // Clear error message if validation passes
        ErrorMessage = string.Empty;
        return true;
    }
}