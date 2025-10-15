using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Tenants.ViewModels;

public partial class TenantInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid tenantId;

    [ObservableProperty]
    [Required(ErrorMessage = "Tenant-Kennung ist erforderlich")]
    [StringLength(64, ErrorMessage = "Tenant-Kennung darf maximal 64 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string identifier = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Anzeigename ist erforderlich")]
    [StringLength(255, ErrorMessage = "Anzeigename darf maximal 255 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string displayName = string.Empty;

    [ObservableProperty]
    [StringLength(255, ErrorMessage = "Domain darf maximal 255 Zeichen haben")]
    private string? domain;

    [ObservableProperty]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse")]
    [StringLength(255, ErrorMessage = "E-Mail darf maximal 255 Zeichen haben")]
    private string? adminEmail;

    [ObservableProperty]
    [StringLength(1024, ErrorMessage = "Connection String darf maximal 1024 Zeichen haben")]
    private string? connectionString;

    [ObservableProperty]
    [StringLength(1024, ErrorMessage = "Beschreibung darf maximal 1024 Zeichen haben")]
    private string? description;

    [ObservableProperty]
    private DateTime? validUntil;

    [ObservableProperty]
    private bool isActive = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    public bool IsEditMode => TenantId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"🏢 Tenant {Identifier} bearbeiten" : "🏢 Neuen Tenant anlegen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToTenantDetail { get; set; }

    public TenantInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(Guid tenantId)
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
        if (!IsEditMode) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<TenantDetailDto>($"superadmin/tenants/{TenantId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync tenants returned null - not authenticated or missing server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                PopulateForm(result.Data);
                _debugService.LogInfo($"Loaded tenant {TenantId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Tenants {TenantId}";
                _debugService.LogError($"Failed to load tenant {TenantId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Tenants: {ex.Message}";
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
        ValidateAllProperties();
        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigiere die markierten Eingaben.";
            return;
        }

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var payload = BuildPayload();

            if (IsEditMode)
            {
            var result = await _httpService.PutAsync<TenantUpsertDto, TenantDetailDto>($"superadmin/tenants/{TenantId}", payload);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    _debugService.LogWarning("PutAsync tenants returned null - not authenticated or missing server URL");
                }
                else if (result.Succeeded && result.Data != null)
                {
                    _debugService.LogInfo($"Successfully updated tenant {TenantId}");
                    if (NavigateToTenantDetail != null)
                    {
                        await NavigateToTenantDetail(TenantId);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Aktualisieren des Tenants";
                    _debugService.LogError($"Failed to update tenant {TenantId}: {ErrorMessage}");
                }
            }
            else
            {
                var result = await _httpService.PostAsync<TenantUpsertDto, TenantDetailDto>("superadmin/tenants", payload);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    _debugService.LogWarning("PostAsync tenants returned null - not authenticated or missing server URL");
                }
                else if (result.Succeeded && result.Data != null)
                {
                    _debugService.LogInfo($"Successfully created tenant {result.Data.Id}");

                    if (NavigateToTenantDetail != null)
                    {
                        await NavigateToTenantDetail(result.Data.Id);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Erstellen des Tenants";
                    _debugService.LogError($"Failed to create tenant: {ErrorMessage}");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des Tenants: {ex.Message}";
            _debugService.LogError(ex, $"Exception saving tenant {TenantId}");
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

    private void PopulateForm(TenantDetailDto dto)
    {
        Identifier = dto.Identifier;
        DisplayName = dto.DisplayName;
        Domain = dto.Domain;
        AdminEmail = dto.AdminEmail;
        ConnectionString = dto.ConnectionString;
        Description = dto.Description;
        ValidUntil = dto.ValidUntil;
        IsActive = dto.IsActive;
    }

    private TenantUpsertDto BuildPayload()
    {
        var normalizedAdminEmail = string.IsNullOrWhiteSpace(AdminEmail) ? null : AdminEmail.Trim();
        var normalizedDomain = string.IsNullOrWhiteSpace(Domain) ? null : Domain.Trim();
        var normalizedConnectionString = string.IsNullOrWhiteSpace(ConnectionString) ? null : ConnectionString.Trim();
        var normalizedDescription = string.IsNullOrWhiteSpace(Description) ? null : Description.Trim();

        return new TenantUpsertDto
        {
            Id = IsEditMode ? TenantId : null,
            TenantCode = Identifier.Trim(),
            Name = DisplayName.Trim(),
            Domain = normalizedDomain,
            AdminEmail = normalizedAdminEmail,
            ContactEmail = normalizedAdminEmail,
            ConnectionString = normalizedConnectionString,
            Description = normalizedDescription,
            ValidUntil = ValidUntil,
            IsActive = IsActive
        };
    }

    private void ClearForm()
    {
        Identifier = string.Empty;
        DisplayName = string.Empty;
        Domain = null;
        AdminEmail = null;
        ConnectionString = null;
        Description = null;
        ValidUntil = null;
        IsActive = true;
        ErrorMessage = string.Empty;
    }
}
