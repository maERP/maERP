using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.User;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Superadmin.ViewModels;

public partial class SuperadminUserDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    private UserDetailDto user = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string userId = string.Empty;

    [ObservableProperty]
    private ObservableCollection<UserTenantAssignmentDto> userTenants = new();

    [ObservableProperty]
    private bool isLoadingTenants;

    [ObservableProperty]
    private ObservableCollection<TenantListDto> availableTenants = new();

    [ObservableProperty]
    private bool isLoadingAvailableTenants;

    [ObservableProperty]
    private bool canManageTenants;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage);
    public string FullName => !string.IsNullOrEmpty(User.Firstname) && !string.IsNullOrEmpty(User.Lastname)
        ? $"{User.Firstname} {User.Lastname}"
        : User.Email;
    public bool HasEmail => !string.IsNullOrEmpty(User.Email);

    public Action? GoBackAction { get; set; }
    public Func<string, Task>? NavigateToEditUser { get; set; }

    public SuperadminUserDetailViewModel(IHttpService httpService, IDebugService debugService, IAuthenticationService authenticationService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _authenticationService = authenticationService;

        // Check if current user can manage tenants (Superadmin)
        CheckTenantManagementPermissions();
    }

    public async Task InitializeAsync(string userId)
    {
        UserId = userId;
        await LoadUserAsync();
        await LoadUserTenantsAsync();

        if (CanManageTenants)
        {
            await LoadAvailableTenantsAsync();
        }
    }

    [RelayCommand]
    private async Task LoadUserAsync()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            ErrorMessage = "Benutzer-ID ist erforderlich";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<UserDetailDto>($"users/{UserId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                User = result.Data;
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(HasEmail));
                _debugService.LogInfo($"Loaded user {User.Email} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.Count > 0 ? result.Messages[0] : "Fehler beim Laden des Benutzers";
                _debugService.LogError($"Failed to load user: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Benutzers: {ex.Message}";
            _debugService.LogError($"Exception loading user: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task LoadUserTenantsAsync()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return;
        }

        IsLoadingTenants = true;

        try
        {
            var result = await _httpService.GetAsync<List<UserTenantAssignmentDto>>($"users/{UserId}/tenants");

            if (result != null && result.Succeeded && result.Data != null)
            {
                UserTenants.Clear();
                foreach (var tenant in result.Data)
                {
                    UserTenants.Add(tenant);
                }
                _debugService.LogInfo($"Loaded {UserTenants.Count} tenant assignments for user");
            }
            else
            {
                _debugService.LogWarning($"Failed to load user tenants: {result?.Messages?.FirstOrDefault() ?? "Unknown error"}");
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"Exception loading user tenants: {ex}");
        }
        finally
        {
            IsLoadingTenants = false;
        }
    }

    private void CheckTenantManagementPermissions()
    {
        // TODO: Implement proper role checking when JWT parsing is available
        // For now, assume all authenticated users can manage tenants
        CanManageTenants = _authenticationService.IsAuthenticated;
    }

    [RelayCommand]
    private async Task LoadAvailableTenantsAsync()
    {
        IsLoadingAvailableTenants = true;

        try
        {
            var result = await _httpService.GetAsync<List<TenantListDto>>("tenants");

            if (result != null && result.Succeeded && result.Data != null)
            {
                AvailableTenants.Clear();

                // Only show tenants that are not already assigned to the user
                var assignedTenantIds = UserTenants.Select(ut => ut.TenantId).ToHashSet();
                var unassignedTenants = result.Data.Where(t => !assignedTenantIds.Contains(t.Id));

                foreach (var tenant in unassignedTenants)
                {
                    AvailableTenants.Add(tenant);
                }

                _debugService.LogInfo($"Loaded {AvailableTenants.Count} available tenants");
            }
            else
            {
                _debugService.LogWarning($"Failed to load available tenants: {result?.Messages?.FirstOrDefault() ?? "Unknown error"}");
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"Exception loading available tenants: {ex}");
        }
        finally
        {
            IsLoadingAvailableTenants = false;
        }
    }

    [RelayCommand]
    private async Task AssignTenantAsync(TenantListDto tenant)
    {
        if (string.IsNullOrEmpty(UserId) || tenant == null)
        {
            return;
        }

        try
        {
            var command = new
            {
                TenantId = tenant.Id,
                IsDefault = false
            };

            var result = await _httpService.PostAsync<object, object>($"users/{UserId}/tenants", command);

            if (result != null && result.Succeeded)
            {
                // Add to assigned tenants
                var newAssignment = new UserTenantAssignmentDto
                {
                    TenantId = tenant.Id,
                    TenantName = tenant.Name,
                    TenantCode = tenant.TenantCode,
                    IsDefault = false,
                    RoleManageUser = false
                };
                UserTenants.Add(newAssignment);

                // Remove from available tenants
                AvailableTenants.Remove(tenant);

                _debugService.LogInfo($"Successfully assigned tenant {tenant.Name} to user");
            }
            else
            {
                _debugService.LogError($"Failed to assign tenant: {result?.Messages?.FirstOrDefault() ?? "Unknown error"}");
                // TODO: Show error message to user
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"Exception assigning tenant: {ex}");
            // TODO: Show error message to user
        }
    }

    [RelayCommand]
    private async Task RemoveTenantAsync(UserTenantAssignmentDto assignment)
    {
        if (string.IsNullOrEmpty(UserId) || assignment == null)
        {
            return;
        }

        try
        {
            var result = await _httpService.DeleteAsync($"users/{UserId}/tenants/{assignment.TenantId}");

            if (result != null && result.Succeeded)
            {
                // Remove from assigned tenants
                UserTenants.Remove(assignment);

                // Add back to available tenants (reload to get full tenant info)
                await LoadAvailableTenantsAsync();

                _debugService.LogInfo($"Successfully removed tenant {assignment.TenantName} from user");
            }
            else
            {
                _debugService.LogError($"Failed to remove tenant: {result?.Messages?.FirstOrDefault() ?? "Unknown error"}");
                // TODO: Show error message to user
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"Exception removing tenant: {ex}");
            // TODO: Show error message to user
        }
    }

    [RelayCommand]
    private async Task SetDefaultTenantAsync(UserTenantAssignmentDto assignment)
    {
        if (string.IsNullOrEmpty(UserId) || assignment == null || assignment.IsDefault)
        {
            return;
        }

        try
        {
            var command = new
            {
                TenantId = assignment.TenantId,
                IsDefault = true
            };

            var result = await _httpService.PostAsync<object, object>($"users/{UserId}/tenants", command);

            if (result != null && result.Succeeded)
            {
                // Update local state
                foreach (var tenant in UserTenants)
                {
                    tenant.IsDefault = tenant.TenantId == assignment.TenantId;
                }

                _debugService.LogInfo($"Successfully set {assignment.TenantName} as default tenant");
            }
            else
            {
                _debugService.LogError($"Failed to set default tenant: {result?.Messages?.FirstOrDefault() ?? "Unknown error"}");
                // TODO: Show error message to user
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"Exception setting default tenant: {ex}");
            // TODO: Show error message to user
        }
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadUserAsync();
        await LoadUserTenantsAsync();

        if (CanManageTenants)
        {
            await LoadAvailableTenantsAsync();
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditUser()
    {
        if (NavigateToEditUser != null && !string.IsNullOrEmpty(UserId))
        {
            await NavigateToEditUser(UserId);
        }
    }

    [RelayCommand]
    private void SendEmail()
    {
        if (HasEmail)
        {
            try
            {
                var emailUri = $"mailto:{User.Email}";
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
