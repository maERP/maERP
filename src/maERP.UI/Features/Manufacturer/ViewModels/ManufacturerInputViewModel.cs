using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Interfaces;
using maERP.UI.Features.Manufacturer.Validators;
using maERP.UI.Services;
using maERP.UI.Shared.Validation;

namespace maERP.UI.Features.Manufacturer.ViewModels;

public partial class ManufacturerInputViewModel : FluentValidationViewModelBase, IManufacturerInputModel
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly ManufacturerClientValidator _validator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid manufacturerId;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string street = string.Empty;

    [ObservableProperty]
    private string city = string.Empty;

    [ObservableProperty]
    private string state = string.Empty;

    [ObservableProperty]
    private string country = string.Empty;

    [ObservableProperty]
    private string zipCode = string.Empty;

    [ObservableProperty]
    private string phone = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string website = string.Empty;

    [ObservableProperty]
    private string logo = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => ManufacturerId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"🏭 Hersteller #{ManufacturerId} bearbeiten" : "🏭 Neuen Hersteller erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    // Validation Error Properties for XAML Binding
    public string? NameError => GetFirstErrorMessage(nameof(Name));
    public string? EmailError => GetFirstErrorMessage(nameof(Email));

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToManufacturerDetail { get; set; }

    public ManufacturerInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _validator = new ManufacturerClientValidator();
    }

    /// <summary>
    /// Gibt den FluentValidator für dieses ViewModel zurück.
    /// </summary>
    protected override IValidator GetValidator() => _validator;

    // Property-Change Validierung für Echtzeit-Feedback
    partial void OnNameChanged(string value) => ValidateProperty(nameof(Name));
    partial void OnEmailChanged(string value) => ValidateProperty(nameof(Email));

    public async Task InitializeAsync(Guid manufacturerId = default)
    {
        ManufacturerId = manufacturerId;

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
        if (ManufacturerId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<ManufacturerDetailDto>($"manufacturers/{ManufacturerId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                PopulateFormFromDto(result.Data);
                _debugService.LogInfo($"Loaded manufacturer {ManufacturerId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Herstellers {ManufacturerId}";
                _debugService.LogError($"Failed to load manufacturer {ManufacturerId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Herstellers: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading manufacturer {ManufacturerId}");
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

        // Validate required fields
        if (string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Herstellername ist erforderlich";
            return;
        }

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var dto = CreateDtoFromForm();

            if (IsEditMode)
            {
                // Update existing manufacturer
                var result = await _httpService.PutAsync<ManufacturerInputDto, Guid>($"manufacturers/{ManufacturerId}", dto);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    _debugService.LogWarning("PutAsync returned null - not authenticated or no server URL");
                }
                else if (result.Succeeded)
                {
                    _debugService.LogInfo($"Successfully updated manufacturer {ManufacturerId}");

                    if (NavigateToManufacturerDetail != null)
                    {
                        await NavigateToManufacturerDetail(ManufacturerId);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Aktualisieren des Herstellers";
                    _debugService.LogError($"Failed to update manufacturer {ManufacturerId}: {ErrorMessage}");
                }
            }
            else
            {
                // Create new manufacturer
                var result = await _httpService.PostAsync<ManufacturerInputDto, ManufacturerDetailDto>("manufacturers", dto);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    _debugService.LogWarning("PostAsync returned null - not authenticated or no server URL");
                }
                else if (result.Succeeded && result.Data != null)
                {
                    _debugService.LogInfo($"Successfully created manufacturer {result.Data.Id}");

                    if (NavigateToManufacturerDetail != null)
                    {
                        await NavigateToManufacturerDetail(result.Data.Id);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Erstellen des Herstellers";
                    _debugService.LogError($"Failed to create manufacturer: {ErrorMessage}");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des Herstellers: {ex.Message}";
            _debugService.LogError(ex, $"Exception saving manufacturer {ManufacturerId}");
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

    private void PopulateFormFromDto(ManufacturerDetailDto dto)
    {
        Name = dto.Name;
        Street = dto.Street;
        City = dto.City;
        State = dto.State;
        Country = dto.Country;
        ZipCode = dto.ZipCode;
        Phone = dto.Phone;
        Email = dto.Email;
        Website = dto.Website;
        Logo = dto.Logo;
    }

    private ManufacturerInputDto CreateDtoFromForm()
    {
        return new ManufacturerInputDto
        {
            Id = ManufacturerId,
            Name = Name.Trim(),
            Street = Street.Trim(),
            City = City.Trim(),
            State = State.Trim(),
            Country = Country.Trim(),
            ZipCode = ZipCode.Trim(),
            Phone = Phone.Trim(),
            Email = Email.Trim(),
            Website = Website.Trim(),
            Logo = Logo.Trim()
        };
    }

    private void ClearForm()
    {
        Name = string.Empty;
        Street = string.Empty;
        City = string.Empty;
        State = string.Empty;
        Country = string.Empty;
        ZipCode = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        Website = string.Empty;
        Logo = string.Empty;
        ErrorMessage = string.Empty;
    }
}