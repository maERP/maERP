using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Manufacturer;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Manufacturer.ViewModels;

public partial class ManufacturerInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid manufacturerId;

    [ObservableProperty]
    [Required(ErrorMessage = "Herstellername ist erforderlich")]
    [StringLength(255, ErrorMessage = "Herstellername darf maximal 255 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string name = string.Empty;

    [ObservableProperty]
    [StringLength(255, ErrorMessage = "Straße darf maximal 255 Zeichen haben")]
    private string street = string.Empty;

    [ObservableProperty]
    [StringLength(255, ErrorMessage = "Stadt darf maximal 255 Zeichen haben")]
    private string city = string.Empty;

    [ObservableProperty]
    [StringLength(255, ErrorMessage = "Bundesland darf maximal 255 Zeichen haben")]
    private string state = string.Empty;

    [ObservableProperty]
    [StringLength(255, ErrorMessage = "Land darf maximal 255 Zeichen haben")]
    private string country = string.Empty;

    [ObservableProperty]
    [StringLength(20, ErrorMessage = "PLZ darf maximal 20 Zeichen haben")]
    private string zipCode = string.Empty;

    [ObservableProperty]
    [StringLength(50, ErrorMessage = "Telefon darf maximal 50 Zeichen haben")]
    private string phone = string.Empty;

    [ObservableProperty]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse")]
    [StringLength(255, ErrorMessage = "E-Mail darf maximal 255 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string email = string.Empty;

    [ObservableProperty]
    [StringLength(500, ErrorMessage = "Website darf maximal 500 Zeichen haben")]
    private string website = string.Empty;

    [ObservableProperty]
    [StringLength(500, ErrorMessage = "Logo URL darf maximal 500 Zeichen haben")]
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

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToManufacturerDetail { get; set; }

    public ManufacturerInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

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