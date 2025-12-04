using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Tenants.Services;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using Microsoft.Extensions.Logging;
using Uno.Extensions.Authentication;

namespace maERP.Client.Features.Tenants.Models;

/// <summary>
/// Model for tenant edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class TenantEditModel : AsyncInitializableModel
{
    private readonly ITenantService _tenantService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly ITenantContextService _tenantContext;
    private readonly IAuthenticationService _authentication;
    private readonly Guid? _tenantId;

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

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    // User Invite UI State
    private bool _isAddUserOverlayOpen;
    private string _inviteEmail = string.Empty;
    private string _inviteErrorMessage = string.Empty;
    private bool _isSearchingUser;
    private bool _isAddingUser;
    private UserListDto? _foundUser;

    public TenantEditModel(
        ITenantService tenantService,
        INavigator navigator,
        IStringLocalizer localizer,
        ITenantContextService tenantContext,
        IAuthenticationService authentication,
        ILogger<TenantEditModel> logger,
        TenantEditData? data = null)
        : base(logger)
    {
        _tenantService = tenantService;
        _navigator = navigator;
        _localizer = localizer;
        _tenantContext = tenantContext;
        _authentication = authentication;
        _tenantId = data?.tenantId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        if (_tenantId.HasValue)
        {
            await LoadTenantAsync(ct);
        }
    }

    public bool IsEditMode => _tenantId.HasValue;

    public string Title => IsEditMode
        ? _localizer["TenantEditPage.TitleEdit"]
        : _localizer["TenantEditPage.TitleNew"];

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
    /// Combined loading state (initializing or saving).
    /// </summary>
    public bool IsLoading => IsInitializing || IsSaving;

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

    #region User Invite State

    /// <summary>
    /// Indicates whether the add user overlay is open.
    /// </summary>
    public bool IsAddUserOverlayOpen
    {
        get => _isAddUserOverlayOpen;
        set => SetProperty(ref _isAddUserOverlayOpen, value);
    }

    /// <summary>
    /// The email address entered in the invite field.
    /// </summary>
    public string InviteEmail
    {
        get => _inviteEmail;
        set
        {
            if (SetProperty(ref _inviteEmail, value))
            {
                // Clear previous search results when email changes
                FoundUser = null;
                InviteErrorMessage = string.Empty;
                OnPropertyChanged(nameof(CanSearchUser));
            }
        }
    }

    /// <summary>
    /// Error message for the user invite operation.
    /// </summary>
    public string InviteErrorMessage
    {
        get => _inviteErrorMessage;
        set => SetProperty(ref _inviteErrorMessage, value);
    }

    /// <summary>
    /// Indicates whether a user search is in progress.
    /// </summary>
    public bool IsSearchingUser
    {
        get => _isSearchingUser;
        private set
        {
            if (SetProperty(ref _isSearchingUser, value))
            {
                OnPropertyChanged(nameof(IsNotSearchingUser));
                OnPropertyChanged(nameof(CanSearchUser));
                OnPropertyChanged(nameof(CanAddUser));
            }
        }
    }

    /// <summary>
    /// Indicates whether a user search is NOT in progress (inverse of IsSearchingUser).
    /// </summary>
    public bool IsNotSearchingUser => !IsSearchingUser;

    /// <summary>
    /// Indicates whether a user add operation is in progress.
    /// </summary>
    public bool IsAddingUser
    {
        get => _isAddingUser;
        private set
        {
            if (SetProperty(ref _isAddingUser, value))
            {
                OnPropertyChanged(nameof(CanAddUser));
            }
        }
    }

    /// <summary>
    /// The user found by the search operation.
    /// </summary>
    public UserListDto? FoundUser
    {
        get => _foundUser;
        private set
        {
            if (SetProperty(ref _foundUser, value))
            {
                OnPropertyChanged(nameof(HasFoundUser));
                OnPropertyChanged(nameof(CanAddUser));
            }
        }
    }

    /// <summary>
    /// Indicates whether a user has been found.
    /// </summary>
    public bool HasFoundUser => FoundUser != null;

    /// <summary>
    /// Indicates whether the user can initiate a search.
    /// </summary>
    public bool CanSearchUser =>
        !string.IsNullOrWhiteSpace(InviteEmail) &&
        !IsSearchingUser &&
        !IsAddingUser;

    /// <summary>
    /// Indicates whether the user can add the found user.
    /// </summary>
    public bool CanAddUser =>
        HasFoundUser &&
        !IsSearchingUser &&
        !IsAddingUser;

    #endregion

    private async Task LoadTenantAsync(CancellationToken ct)
    {
        if (!_tenantId.HasValue) return;

        var tenant = await _tenantService.GetTenantAsync(_tenantId.Value, ct);
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

            if (_tenantId.HasValue)
            {
                // Edit mode: Update existing tenant
                input.Id = _tenantId.Value;
                await _tenantService.UpdateTenantAsync(_tenantId.Value, input, ct);

                // Refresh tenant list after update (handles deactivation)
                var hasTenantsRemaining = await _tenantContext.RefreshTenantsAndCheckAvailabilityAsync(ct);

                if (!hasTenantsRemaining)
                {
                    // No tenants remaining (user deactivated their only tenant) - log out
                    await _authentication.LogoutAsync(ct);
                    return;
                }
            }
            else
            {
                // Create mode: Create new tenant
                await _tenantService.CreateTenantAsync(input, ct);

                // Refresh tenant list after creating a new tenant
                await _tenantContext.RefreshTenantsAsync(ct);
            }

            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["TenantEditPage.Error.SaveFailed"], ex.Message);
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
    /// Opens the add user overlay.
    /// </summary>
    public void OpenAddUserOverlay()
    {
        InviteEmail = string.Empty;
        InviteErrorMessage = string.Empty;
        FoundUser = null;
        IsAddUserOverlayOpen = true;
    }

    /// <summary>
    /// Closes the add user overlay.
    /// </summary>
    public void CloseAddUserOverlay()
    {
        IsAddUserOverlayOpen = false;
        InviteEmail = string.Empty;
        InviteErrorMessage = string.Empty;
        FoundUser = null;
    }

    /// <summary>
    /// Searches for a user by email address.
    /// </summary>
    public async Task SearchUserAsync(CancellationToken ct = default)
    {
        if (!CanSearchUser || !_tenantId.HasValue) return;

        IsSearchingUser = true;
        InviteErrorMessage = string.Empty;
        FoundUser = null;

        try
        {
            var user = await _tenantService.SearchUserByEmailAsync(_tenantId.Value, InviteEmail, ct);
            FoundUser = user;
        }
        catch (ApiException ex)
        {
            InviteErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            InviteErrorMessage = string.Format(_localizer["TenantEditPage.Error.SearchUserFailed"], ex.Message);
        }
        finally
        {
            IsSearchingUser = false;
        }
    }

    /// <summary>
    /// Adds the found user to the tenant.
    /// </summary>
    public async Task AddUserToTenantAsync(CancellationToken ct = default)
    {
        if (!CanAddUser || !_tenantId.HasValue || FoundUser == null) return;

        IsAddingUser = true;
        InviteErrorMessage = string.Empty;

        try
        {
            await _tenantService.AddUserToTenantAsync(_tenantId.Value, InviteEmail, ct);
            CloseAddUserOverlay();
        }
        catch (ApiException ex)
        {
            InviteErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            InviteErrorMessage = string.Format(_localizer["TenantEditPage.Error.AddUserFailed"], ex.Message);
        }
        finally
        {
            IsAddingUser = false;
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
/// Navigation data for tenant edit page.
/// </summary>
public record TenantEditData(Guid tenantId);
