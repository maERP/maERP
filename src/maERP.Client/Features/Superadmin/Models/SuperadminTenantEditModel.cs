using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Superadmin.Services;
using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Superadmin.Models;

/// <summary>
/// Model for superadmin tenant edit page.
/// Only supports editing existing tenants, not creating new ones.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class SuperadminTenantEditModel : AsyncInitializableModel
{
    private readonly ISuperadminTenantService _tenantService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid _tenantId;

    // Basic Information
    private string _name = string.Empty;
    private string _description = string.Empty;
    private bool _isActive = true;

    // Company Information
    private string _companyName = string.Empty;
    private string _contactEmail = string.Empty;
    private string _phone = string.Empty;
    private string _website = string.Empty;

    // Address
    private string _street = string.Empty;
    private string _street2 = string.Empty;
    private string _postalCode = string.Empty;
    private string _city = string.Empty;
    private string _state = string.Empty;
    private string _country = string.Empty;

    // Payment Information
    private string _iban = string.Empty;

    // Users
    private ObservableCollection<SuperadminTenantUserDto> _users = new();
    private ObservableCollection<UserListDto> _availableUsers = new();
    private UserListDto? _selectedUserToAdd;
    private bool _isLoadingAvailableUsers;

    // UI State
    private bool _isSaving;
    private bool _isUserOperationInProgress;
    private string _errorMessage = string.Empty;

    public SuperadminTenantEditModel(
        ISuperadminTenantService tenantService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<SuperadminTenantEditModel> logger,
        SuperadminTenantEditData data)
        : base(logger)
    {
        _tenantService = tenantService;
        _navigator = navigator;
        _localizer = localizer;
        _tenantId = data.tenantId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        await LoadTenantAsync(ct);
    }

    public string Title => _localizer["SuperadminTenantEditPage.Title"];

    #region Basic Information

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }

    #endregion

    #region Company Information

    public string CompanyName
    {
        get => _companyName;
        set => SetProperty(ref _companyName, value);
    }

    public string ContactEmail
    {
        get => _contactEmail;
        set => SetProperty(ref _contactEmail, value);
    }

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    public string Website
    {
        get => _website;
        set => SetProperty(ref _website, value);
    }

    #endregion

    #region Address

    public string Street
    {
        get => _street;
        set => SetProperty(ref _street, value);
    }

    public string Street2
    {
        get => _street2;
        set => SetProperty(ref _street2, value);
    }

    public string PostalCode
    {
        get => _postalCode;
        set => SetProperty(ref _postalCode, value);
    }

    public string City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }

    public string State
    {
        get => _state;
        set => SetProperty(ref _state, value);
    }

    public string Country
    {
        get => _country;
        set => SetProperty(ref _country, value);
    }

    #endregion

    #region Payment Information

    public string Iban
    {
        get => _iban;
        set => SetProperty(ref _iban, value);
    }

    #endregion

    #region Users

    /// <summary>
    /// Collection of users assigned to this tenant.
    /// </summary>
    public ObservableCollection<SuperadminTenantUserDto> Users
    {
        get => _users;
        private set => SetProperty(ref _users, value);
    }

    /// <summary>
    /// Indicates whether there are users assigned to this tenant.
    /// </summary>
    public bool HasUsers => Users.Count > 0;

    /// <summary>
    /// Number of users assigned to this tenant.
    /// </summary>
    public int UserCount => Users.Count;

    /// <summary>
    /// Collection of users available for assignment (not yet assigned to this tenant).
    /// </summary>
    public ObservableCollection<UserListDto> AvailableUsers
    {
        get => _availableUsers;
        private set => SetProperty(ref _availableUsers, value);
    }

    /// <summary>
    /// The user selected to be added to this tenant.
    /// </summary>
    public UserListDto? SelectedUserToAdd
    {
        get => _selectedUserToAdd;
        set
        {
            if (SetProperty(ref _selectedUserToAdd, value))
            {
                OnPropertyChanged(nameof(CanAddUser));
            }
        }
    }

    /// <summary>
    /// Indicates whether available users are being loaded.
    /// </summary>
    public bool IsLoadingAvailableUsers
    {
        get => _isLoadingAvailableUsers;
        private set => SetProperty(ref _isLoadingAvailableUsers, value);
    }

    /// <summary>
    /// Indicates whether the selected user can be added.
    /// </summary>
    public bool CanAddUser => SelectedUserToAdd != null && !IsUserOperationInProgress;

    /// <summary>
    /// Indicates whether there are no available users to add.
    /// </summary>
    public bool HasNoAvailableUsers => AvailableUsers.Count == 0;

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
    /// Indicates whether a user operation (add/remove) is in progress.
    /// </summary>
    public bool IsUserOperationInProgress
    {
        get => _isUserOperationInProgress;
        private set
        {
            if (SetProperty(ref _isUserOperationInProgress, value))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
            }
        }
    }

    /// <summary>
    /// Combined loading state (initializing, saving, or user operation).
    /// </summary>
    public bool IsLoading => IsInitializing || IsSaving || IsUserOperationInProgress;

    /// <summary>
    /// Inverse of IsLoading for binding convenience.
    /// </summary>
    public bool IsNotLoading => !IsLoading;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool CanSave =>
        !string.IsNullOrWhiteSpace(Name) &&
        !IsLoading;

    #endregion

    private async Task LoadTenantAsync(CancellationToken ct)
    {
        var tenant = await _tenantService.GetTenantDetailAsync(_tenantId, ct);
        if (tenant != null)
        {
            // Basic Information
            Name = tenant.Name;
            Description = tenant.Description ?? string.Empty;
            IsActive = tenant.IsActive;

            // Company Information
            CompanyName = tenant.CompanyName ?? string.Empty;
            ContactEmail = tenant.ContactEmail ?? string.Empty;
            Phone = tenant.Phone ?? string.Empty;
            Website = tenant.Website ?? string.Empty;

            // Address
            Street = tenant.Street ?? string.Empty;
            Street2 = tenant.Street2 ?? string.Empty;
            PostalCode = tenant.PostalCode ?? string.Empty;
            City = tenant.City ?? string.Empty;
            State = tenant.State ?? string.Empty;
            Country = tenant.Country ?? string.Empty;

            // Payment Information
            Iban = tenant.Iban ?? string.Empty;

            // Users
            Users = new ObservableCollection<SuperadminTenantUserDto>(tenant.Users);
            OnPropertyChanged(nameof(HasUsers));
            OnPropertyChanged(nameof(UserCount));
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new TenantInputDto
            {
                Id = _tenantId,
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

            await _tenantService.UpdateTenantAsync(_tenantId, input, ct);
            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["SuperadminTenantEditPage.Error.SaveFailed"], ex.Message);
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

    /// <summary>
    /// Removes a user from this tenant.
    /// </summary>
    /// <param name="user">The user to remove.</param>
    public async Task RemoveUserAsync(SuperadminTenantUserDto user)
    {
        if (IsUserOperationInProgress) return;

        IsUserOperationInProgress = true;
        ErrorMessage = string.Empty;

        try
        {
            await _tenantService.RemoveUserFromTenantAsync(user.Id, _tenantId);

            // Remove from local collection
            Users.Remove(user);
            OnPropertyChanged(nameof(HasUsers));
            OnPropertyChanged(nameof(UserCount));
        }
        catch (ApiException ex)
        {
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["SuperadminTenantEditPage.Error.RemoveUserFailed"], ex.Message);
        }
        finally
        {
            IsUserOperationInProgress = false;
        }
    }

    /// <summary>
    /// Loads users that are available for assignment to this tenant.
    /// Filters out users that are already assigned.
    /// </summary>
    public async Task LoadAvailableUsersAsync()
    {
        if (IsLoadingAvailableUsers) return;

        IsLoadingAvailableUsers = true;
        ErrorMessage = string.Empty;

        try
        {
            var allUsers = await _tenantService.GetAllUsersAsync();

            // Get IDs of users already assigned to this tenant
            var assignedUserIds = Users.Select(u => u.Id).ToHashSet();

            // Filter out already assigned users
            var available = allUsers
                .Where(u => !assignedUserIds.Contains(u.Id))
                .OrderBy(u => u.Lastname)
                .ThenBy(u => u.Firstname)
                .ToList();

            AvailableUsers = new ObservableCollection<UserListDto>(available);
            SelectedUserToAdd = null;
            OnPropertyChanged(nameof(HasNoAvailableUsers));
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["SuperadminTenantEditPage.Error.LoadUsersFailed"], ex.Message);
        }
        finally
        {
            IsLoadingAvailableUsers = false;
        }
    }

    /// <summary>
    /// Adds the selected user to this tenant.
    /// </summary>
    public async Task AddSelectedUserAsync()
    {
        if (!CanAddUser || SelectedUserToAdd == null) return;

        IsUserOperationInProgress = true;
        ErrorMessage = string.Empty;

        try
        {
            await _tenantService.AssignUserToTenantAsync(SelectedUserToAdd.Id, _tenantId);

            // Add to local collection
            var newUser = new SuperadminTenantUserDto
            {
                Id = SelectedUserToAdd.Id,
                Email = SelectedUserToAdd.Email,
                Firstname = SelectedUserToAdd.Firstname,
                Lastname = SelectedUserToAdd.Lastname,
                IsDefault = false,
                RoleManageTenant = false,
                RoleManageUser = false,
                DateCreated = SelectedUserToAdd.DateCreated
            };

            Users.Add(newUser);
            OnPropertyChanged(nameof(HasUsers));
            OnPropertyChanged(nameof(UserCount));

            // Remove from available users
            AvailableUsers.Remove(SelectedUserToAdd);
            SelectedUserToAdd = null;
            OnPropertyChanged(nameof(HasNoAvailableUsers));
        }
        catch (ApiException ex)
        {
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["SuperadminTenantEditPage.Error.AddUserFailed"], ex.Message);
        }
        finally
        {
            IsUserOperationInProgress = false;
        }
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        // Update dependent properties
        if (propertyName is nameof(Name))
        {
            base.OnPropertyChanged(nameof(CanSave));
        }

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}

/// <summary>
/// Navigation data for superadmin tenant edit page.
/// </summary>
public record SuperadminTenantEditData(Guid tenantId);
