using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Interfaces;
using maERP.UI.Features.Tenant.Validators;
using maERP.UI.Services;
using maERP.UI.Shared.Validation;

namespace maERP.UI.Features.Tenant.ViewModels;

public partial class TenantInputViewModel : FluentValidationViewModelBase, ITenantInputModel
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly TenantClientValidator _validator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid tenantId;

    // Basic Information
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private bool isActive = true;

    // Company Information
    [ObservableProperty]
    private string companyName = string.Empty;

    // Contact Information
    [ObservableProperty]
    private string contactEmail = string.Empty;

    [ObservableProperty]
    private string phone = string.Empty;

    [ObservableProperty]
    private string website = string.Empty;

    // Address Information
    [ObservableProperty]
    private string street = string.Empty;

    [ObservableProperty]
    private string street2 = string.Empty;

    [ObservableProperty]
    private string postalCode = string.Empty;

    [ObservableProperty]
    private string city = string.Empty;

    [ObservableProperty]
    private string state = string.Empty;

    [ObservableProperty]
    private string country = string.Empty;

    // Banking Information
    [ObservableProperty]
    private string iban = string.Empty;

    // UI State
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => TenantId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"Mandant bearbeiten" : "Neuen Mandanten erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    // Validation Error Properties for XAML Binding
    public string? NameError => GetFirstErrorMessage(nameof(Name));
    public string? ContactEmailError => GetFirstErrorMessage(nameof(ContactEmail));
    public string? DescriptionError => GetFirstErrorMessage(nameof(Description));

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToTenantDetail { get; set; }

    public TenantInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _validator = new TenantClientValidator();
    }

    /// <summary>
    /// Gibt den FluentValidator für dieses ViewModel zurück.
    /// </summary>
    protected override IValidator GetValidator() => _validator;

    // Property-Change Validierung für Echtzeit-Feedback
    partial void OnNameChanged(string value) => ValidateProperty(nameof(Name));
    partial void OnContactEmailChanged(string value) => ValidateProperty(nameof(ContactEmail));

    public async Task InitializeAsync(Guid tenantId = default)
    {
        TenantId = tenantId;

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
        if (TenantId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<TenantDetailDto>($"tenants/{TenantId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var tenant = result.Data;

                // Map tenant data to form fields
                Name = tenant.Name;
                Description = tenant.Description;
                IsActive = tenant.IsActive;
                CompanyName = tenant.CompanyName ?? string.Empty;
                ContactEmail = tenant.ContactEmail ?? string.Empty;
                Phone = tenant.Phone ?? string.Empty;
                Website = tenant.Website ?? string.Empty;
                Street = tenant.Street ?? string.Empty;
                Street2 = tenant.Street2 ?? string.Empty;
                PostalCode = tenant.PostalCode ?? string.Empty;
                City = tenant.City ?? string.Empty;
                State = tenant.State ?? string.Empty;
                Country = tenant.Country ?? string.Empty;
                Iban = tenant.Iban ?? string.Empty;

                _debugService.LogInfo($"Loaded tenant {TenantId} for editing successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Mandanten {TenantId}";
                _debugService.LogError($"Failed to load tenant {TenantId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Mandanten: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading tenant {TenantId}");
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
            var tenantDto = new TenantInputDto
            {
                Name = Name,
                Description = Description,
                IsActive = IsActive,
                CompanyName = string.IsNullOrWhiteSpace(CompanyName) ? null : CompanyName,
                ContactEmail = string.IsNullOrWhiteSpace(ContactEmail) ? null : ContactEmail,
                Phone = string.IsNullOrWhiteSpace(Phone) ? null : Phone,
                Website = string.IsNullOrWhiteSpace(Website) ? null : Website,
                Street = string.IsNullOrWhiteSpace(Street) ? null : Street,
                Street2 = string.IsNullOrWhiteSpace(Street2) ? null : Street2,
                PostalCode = string.IsNullOrWhiteSpace(PostalCode) ? null : PostalCode,
                City = string.IsNullOrWhiteSpace(City) ? null : City,
                State = string.IsNullOrWhiteSpace(State) ? null : State,
                Country = string.IsNullOrWhiteSpace(Country) ? null : Country,
                Iban = string.IsNullOrWhiteSpace(Iban) ? null : Iban
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<TenantInputDto, Guid>($"tenants/{TenantId}", tenantDto)
                : await _httpService.PostAsync<TenantInputDto, Guid>("tenants", tenantDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Tenant {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToTenantDetail != null)
                {
                    await NavigateToTenantDetail(TenantId);
                }
                else if (!IsEditMode && result.Data != Guid.Empty)
                {
                    // Navigate to detail view of newly created tenant
                    if (NavigateToTenantDetail != null)
                    {
                        await NavigateToTenantDetail(result.Data);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    GoBackAction?.Invoke();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Speichern des Mandanten";
                _debugService.LogError($"Failed to save tenant: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des Mandanten: {ex.Message}";
            _debugService.LogError(ex, "Exception saving tenant");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        _debugService.LogInfo("Tenant edit cancelled");
        GoBackAction?.Invoke();
    }

    private bool ValidateForm()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Fehler im Formular.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Mandantenname ist erforderlich.";
            return false;
        }

        return true;
    }

    private void ClearForm()
    {
        Name = string.Empty;
        Description = string.Empty;
        IsActive = true;
        CompanyName = string.Empty;
        ContactEmail = string.Empty;
        Phone = string.Empty;
        Website = string.Empty;
        Street = string.Empty;
        Street2 = string.Empty;
        PostalCode = string.Empty;
        City = string.Empty;
        State = string.Empty;
        Country = string.Empty;
        Iban = string.Empty;
        ErrorMessage = string.Empty;
    }
}
