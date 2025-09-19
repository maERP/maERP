using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Administration.ViewModels;

public partial class TenantDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    private TenantDetailDto tenant = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string tenantId = string.Empty;

    [ObservableProperty]
    private bool canManageTenants;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage);
    public bool IsActive => Tenant.IsActive;
    public bool HasContactEmail => !string.IsNullOrEmpty(Tenant.ContactEmail);
    public string FormattedCreationDate => Tenant.DateCreated.ToString("dd.MM.yyyy HH:mm");
    public string FormattedModificationDate => Tenant.DateModified.ToString("dd.MM.yyyy HH:mm");

    public Action? GoBackAction { get; set; }
    public Func<string, Task>? NavigateToEditTenant { get; set; }

    public TenantDetailViewModel(IHttpService httpService, IDebugService debugService, IAuthenticationService authenticationService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _authenticationService = authenticationService;

        CheckSuperadminPermissions();
    }

    public async Task InitializeAsync(string tenantId)
    {
        TenantId = tenantId;
        await LoadTenantAsync();
    }

    [RelayCommand]
    private async Task LoadTenantAsync()
    {
        if (string.IsNullOrEmpty(TenantId))
        {
            ErrorMessage = "Tenant-ID ist erforderlich";
            return;
        }

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
                Tenant = result.Data;
                OnPropertyChanged(nameof(IsActive));
                OnPropertyChanged(nameof(HasContactEmail));
                OnPropertyChanged(nameof(FormattedCreationDate));
                OnPropertyChanged(nameof(FormattedModificationDate));
                _debugService.LogInfo($"Loaded tenant {Tenant.Name} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.Count > 0 ? result.Messages[0] : "Fehler beim Laden des Tenants";
                _debugService.LogError($"Failed to load tenant: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Tenants: {ex.Message}";
            _debugService.LogError($"Exception loading tenant: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void CheckSuperadminPermissions()
    {
        try
        {
            if (!_authenticationService.IsAuthenticated || string.IsNullOrEmpty(_authenticationService.Token))
            {
                CanManageTenants = false;
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(_authenticationService.Token);

            var roleClaims = jwtToken.Claims.Where(c => c.Type == "role" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();

            CanManageTenants = roleClaims.Any(c => c.Value.Equals("Superadmin", StringComparison.OrdinalIgnoreCase));

            _debugService.LogInfo($"Tenant management permission: {CanManageTenants}");
        }
        catch (Exception ex)
        {
            _debugService.LogError($"Error checking superadmin permissions: {ex.Message}");
            CanManageTenants = false;
        }
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadTenantAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditTenant()
    {
        if (NavigateToEditTenant != null && !string.IsNullOrEmpty(TenantId))
        {
            await NavigateToEditTenant(TenantId);
        }
    }

    [RelayCommand]
    private void SendEmail()
    {
        if (HasContactEmail)
        {
            try
            {
                var emailUri = $"mailto:{Tenant.ContactEmail}";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = emailUri,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                _debugService.LogError($"Failed to open email client: {ex.Message}");
            }
        }
    }
}