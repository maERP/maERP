using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Interfaces;
using maERP.UI.Features.Warehouses.Validators;
using maERP.UI.Services;
using maERP.UI.Shared.Validation;

namespace maERP.UI.Features.Warehouses.ViewModels;

public partial class WarehouseInputViewModel : FluentValidationViewModelBase, IWarehouseInputModel
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly WarehouseClientValidator _validator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool isSaving;

    [ObservableProperty]
    private Guid id;

    [ObservableProperty]
    private string name = string.Empty;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage);
    public bool IsEditMode => Id != Guid.Empty;
    public string PageTitle => IsEditMode ? $"🏭 Lager #{Id} bearbeiten" : "🏭 Neues Lager erstellen";

    // Validation Error Properties for XAML Binding
    public string? NameError => GetFirstErrorMessage(nameof(Name));

    public Action? GoBackAction { get; set; }
    public Action? OnSaveSuccessAction { get; set; }

    public WarehouseInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _validator = new WarehouseClientValidator();
    }

    /// <summary>
    /// Gibt den FluentValidator für dieses ViewModel zurück.
    /// </summary>
    protected override IValidator GetValidator() => _validator;

    // Property-Change Validierung für Echtzeit-Feedback
    partial void OnNameChanged(string value) => ValidateProperty(nameof(Name));

    public async Task InitializeAsync(Guid? warehouseId = null)
    {
        if (warehouseId.HasValue && warehouseId.Value != Guid.Empty)
        {
            Id = warehouseId.Value;
            await LoadWarehouseAsync();
        }
        else
        {
            // New warehouse
            Id = Guid.Empty;
            Name = string.Empty;
            ClearErrors();
            OnPropertyChanged(nameof(IsEditMode));
            OnPropertyChanged(nameof(PageTitle));
        }
    }

    [RelayCommand]
    private async Task LoadWarehouseAsync()
    {
        if (Id == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<WarehouseDetailDto>($"warehouses/{Id}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var warehouse = result.Data;
                Name = warehouse.Name;
                ClearErrors();
                _debugService.LogInfo($"Loaded warehouse {Id} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Lagers {Id}";
                _debugService.LogError($"Failed to load warehouse {Id}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Lagers: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading warehouse {Id}");
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

        // Validate input
        if (!ValidateInput())
        {
            return;
        }

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var warehouseDto = new WarehouseInputDto
            {
                Id = Id,
                Name = Name.Trim()
            };

            if (IsEditMode)
            {
                // Update existing warehouse
                var result = await _httpService.PutAsync<WarehouseInputDto, Guid>($"warehouses/{Id}", warehouseDto);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    _debugService.LogWarning("PutAsync returned null - not authenticated or no server URL");
                }
                else if (result.Succeeded)
                {
                    _debugService.LogInfo($"Successfully updated warehouse {Id}");
                    OnSaveSuccessAction?.Invoke();
                }
                else
                {
                    ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Aktualisieren des Lagers";
                    _debugService.LogError($"Failed to update warehouse {Id}: {ErrorMessage}");
                }
            }
            else
            {
                // Create new warehouse
                var result = await _httpService.PostAsync<WarehouseInputDto, WarehouseDetailDto>("warehouses", warehouseDto);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    _debugService.LogWarning("PostAsync returned null - not authenticated or no server URL");
                }
                else if (result.Succeeded && result.Data != null)
                {
                    _debugService.LogInfo($"Successfully created warehouse with ID {result.Data.Id}");
                    OnSaveSuccessAction?.Invoke();
                }
                else
                {
                    ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Erstellen des Lagers";
                    _debugService.LogError($"Failed to create warehouse: {ErrorMessage}");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving warehouse");
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
    private void ManageStock()
    {
        // TODO: Implement stock management navigation
        _debugService.LogInfo($"Managing stock for warehouse {Id}");
    }

    [RelayCommand]
    private void ManageSalesChannels()
    {
        // TODO: Implement sales channel management navigation
        _debugService.LogInfo($"Managing sales channels for warehouse {Id}");
    }

    private bool ValidateInput()
    {
        // Use FluentValidation
        return ValidateAllProperties();
    }
}