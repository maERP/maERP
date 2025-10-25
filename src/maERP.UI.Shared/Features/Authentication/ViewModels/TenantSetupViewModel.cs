using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Authentication.ViewModels;

public partial class TenantSetupViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [Required(ErrorMessage = "Mandantenname ist erforderlich")]
    [StringLength(255, ErrorMessage = "Mandantenname darf maximal 255 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string name = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    public bool ShouldShowContent => !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    public event Action? OnTenantCreated;
    public event Action? OnLogout;

    public TenantSetupViewModel(IHttpService httpService, IAuthenticationService authenticationService, IDebugService debugService)
    {
        _httpService = httpService;
        _authenticationService = authenticationService;
        _debugService = debugService;
    }

    [RelayCommand]
    private async Task CreateTenantAsync()
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
            var payload = new TenantInputDto
            {
                Name = Name.Trim(),
                Description = string.Empty,
                ContactEmail = null,
                IsActive = true
            };

            var result = await _httpService.PostAsync<TenantInputDto, Guid>("tenants", payload);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("PostAsync tenants returned null - not authenticated or missing server URL");
            }
            else if (result.Succeeded && result.Data != Guid.Empty)
            {
                _debugService.LogInfo($"Successfully created tenant {result.Data}");

                // Trigger re-login to refresh available tenants
                OnTenantCreated?.Invoke();
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Erstellen des Tenants";
                _debugService.LogError($"Failed to create tenant: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des Tenants: {ex.Message}";
            _debugService.LogError(ex, "Exception saving tenant");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Logout()
    {
        OnLogout?.Invoke();
    }
}
