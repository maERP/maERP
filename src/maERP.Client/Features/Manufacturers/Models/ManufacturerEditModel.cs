using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Manufacturers.Services;
using maERP.Domain.Dtos.Manufacturer;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Manufacturers.Models;

/// <summary>
/// Model for manufacturer edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class ManufacturerEditModel : AsyncInitializableModel
{
    private readonly IManufacturerService _manufacturerService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _manufacturerId;

    // Basic Information
    private string _name = string.Empty;

    // Address Information
    private string _street = string.Empty;
    private string _city = string.Empty;
    private string _state = string.Empty;
    private string _country = string.Empty;
    private string _zipCode = string.Empty;

    // Contact Information
    private string _phone = string.Empty;
    private string _email = string.Empty;
    private string _website = string.Empty;

    // Additional Information
    private string _logo = string.Empty;

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public ManufacturerEditModel(
        IManufacturerService manufacturerService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<ManufacturerEditModel> logger,
        ManufacturerEditData? data = null)
        : base(logger)
    {
        _manufacturerService = manufacturerService;
        _navigator = navigator;
        _localizer = localizer;
        _manufacturerId = data?.manufacturerId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        if (_manufacturerId.HasValue)
        {
            await LoadManufacturerAsync(ct);
        }
    }

    public bool IsEditMode => _manufacturerId.HasValue;

    public string Title => IsEditMode
        ? _localizer["ManufacturerEditPage.TitleEdit"]
        : _localizer["ManufacturerEditPage.TitleNew"];

    #region Basic Information

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    #endregion

    #region Address Information

    public string Street
    {
        get => _street;
        set => SetProperty(ref _street, value);
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

    public string ZipCode
    {
        get => _zipCode;
        set => SetProperty(ref _zipCode, value);
    }

    #endregion

    #region Contact Information

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Website
    {
        get => _website;
        set => SetProperty(ref _website, value);
    }

    #endregion

    #region Additional Information

    public string Logo
    {
        get => _logo;
        set => SetProperty(ref _logo, value);
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

    private async Task LoadManufacturerAsync(CancellationToken ct)
    {
        if (!_manufacturerId.HasValue) return;

        var manufacturer = await _manufacturerService.GetManufacturerAsync(_manufacturerId.Value, ct);
        if (manufacturer != null)
        {
            // Basic Information
            Name = manufacturer.Name;

            // Address Information
            Street = manufacturer.Street ?? string.Empty;
            City = manufacturer.City ?? string.Empty;
            State = manufacturer.State ?? string.Empty;
            Country = manufacturer.Country ?? string.Empty;
            ZipCode = manufacturer.ZipCode ?? string.Empty;

            // Contact Information
            Phone = manufacturer.Phone ?? string.Empty;
            Email = manufacturer.Email ?? string.Empty;
            Website = manufacturer.Website ?? string.Empty;

            // Additional Information
            Logo = manufacturer.Logo ?? string.Empty;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new ManufacturerInputDto
            {
                Name = Name,
                Street = Street,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode,
                Phone = Phone,
                Email = Email,
                Website = Website,
                Logo = Logo
            };

            if (_manufacturerId.HasValue)
            {
                input.Id = _manufacturerId.Value;
                await _manufacturerService.UpdateManufacturerAsync(_manufacturerId.Value, input, ct);
            }
            else
            {
                await _manufacturerService.CreateManufacturerAsync(input, ct);
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
            ErrorMessage = string.Format(_localizer["ManufacturerEditPage.Error.SaveFailed"], ex.Message);
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
/// Navigation data for manufacturer edit page.
/// </summary>
public record ManufacturerEditData(Guid? manufacturerId = null);
