using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Features.Countries.Services;
using maERP.Client.Features.Customers.Services;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Enums;

namespace maERP.Client.Features.Customers.Models;

/// <summary>
/// Model for customer edit/create page.
/// Uses INotifyPropertyChanged for form binding.
/// </summary>
public class CustomerEditModel : INotifyPropertyChanged
{
    private readonly ICustomerService _customerService;
    private readonly ICountryService _countryService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _customerId;

    // Personal Information
    private string _firstname = string.Empty;
    private string _lastname = string.Empty;

    // Contact Information
    private string _email = string.Empty;
    private string _phone = string.Empty;
    private string _website = string.Empty;

    // Business Information
    private string _companyName = string.Empty;
    private string _vatNumber = string.Empty;

    // Status and Notes
    private CustomerStatus _customerStatus = CustomerStatus.Active;
    private string _note = string.Empty;

    // Addresses
    private ObservableCollection<EditableCustomerAddress> _addresses = new();
    private EditableCustomerAddress? _editingAddress;
    private bool _isAddressDialogOpen;
    private bool _isNewAddress;

    // Countries
    private ObservableCollection<CountryListDto> _countries = new();

    // UI State
    private bool _isLoading;
    private string _errorMessage = string.Empty;

    public CustomerEditModel(
        ICustomerService customerService,
        ICountryService countryService,
        INavigator navigator,
        IStringLocalizer localizer,
        CustomerEditData? data = null)
    {
        _customerService = customerService;
        _countryService = countryService;
        _navigator = navigator;
        _localizer = localizer;
        _customerId = data?.customerId;

        // Load countries first, then customer data if editing
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await LoadCountriesAsync();

        if (_customerId.HasValue)
        {
            await LoadCustomerAsync();
        }
    }

    private async Task LoadCountriesAsync()
    {
        try
        {
            var countries = await _countryService.GetCountriesAsync();
            Countries.Clear();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
        catch (Exception ex)
        {
            // Log error but don't fail - countries are needed for address dialog
            System.Diagnostics.Debug.WriteLine($"Failed to load countries: {ex.Message}");
        }
    }

    public bool IsEditMode => _customerId.HasValue;

    public string Title => IsEditMode
        ? _localizer["CustomerEditPage.Title.Edit"]
        : _localizer["CustomerEditPage.Title.New"];

    /// <summary>
    /// Available customer status options for the ComboBox.
    /// </summary>
    public IReadOnlyList<CustomerStatusOption> CustomerStatusOptions { get; } = new List<CustomerStatusOption>
    {
        new(CustomerStatus.Active, "CustomerStatus.Active"),
        new(CustomerStatus.Inactive, "CustomerStatus.Inactive"),
        new(CustomerStatus.NoDoi, "CustomerStatus.NoDoi")
    };

    #region Personal Information

    public string Firstname
    {
        get => _firstname;
        set => SetProperty(ref _firstname, value);
    }

    public string Lastname
    {
        get => _lastname;
        set => SetProperty(ref _lastname, value);
    }

    #endregion

    #region Contact Information

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
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

    #region Business Information

    public string CompanyName
    {
        get => _companyName;
        set => SetProperty(ref _companyName, value);
    }

    public string VatNumber
    {
        get => _vatNumber;
        set => SetProperty(ref _vatNumber, value);
    }

    #endregion

    #region Status and Notes

    public CustomerStatus CustomerStatus
    {
        get => _customerStatus;
        set => SetProperty(ref _customerStatus, value);
    }

    public string Note
    {
        get => _note;
        set => SetProperty(ref _note, value);
    }

    #endregion

    #region Addresses

    public ObservableCollection<EditableCustomerAddress> Addresses
    {
        get => _addresses;
        set => SetProperty(ref _addresses, value);
    }

    public EditableCustomerAddress? EditingAddress
    {
        get => _editingAddress;
        set => SetProperty(ref _editingAddress, value);
    }

    public bool IsAddressDialogOpen
    {
        get => _isAddressDialogOpen;
        set => SetProperty(ref _isAddressDialogOpen, value);
    }

    public bool IsNewAddress
    {
        get => _isNewAddress;
        set => SetProperty(ref _isNewAddress, value);
    }

    public string AddressDialogTitle => IsNewAddress
        ? _localizer["AddressDialog.TitleNew"]
        : _localizer["AddressDialog.TitleEdit"];

    public bool HasAddresses => Addresses.Count > 0;

    /// <summary>
    /// Available countries for address selection.
    /// </summary>
    public ObservableCollection<CountryListDto> Countries
    {
        get => _countries;
        set => SetProperty(ref _countries, value);
    }

    // Localized strings for address dialog (using flat key structure for IStringLocalizer compatibility)
    public string AddressFieldFirstname => _localizer["AddressDialog.Firstname"];
    public string AddressFieldFirstnamePlaceholder => _localizer["AddressDialog.FirstnamePlaceholder"];
    public string AddressFieldLastname => _localizer["AddressDialog.Lastname"];
    public string AddressFieldLastnamePlaceholder => _localizer["AddressDialog.LastnamePlaceholder"];
    public string AddressFieldCompanyName => _localizer["AddressDialog.CompanyName"];
    public string AddressFieldCompanyNamePlaceholder => _localizer["AddressDialog.CompanyNamePlaceholder"];
    public string AddressFieldStreet => _localizer["AddressDialog.Street"];
    public string AddressFieldStreetPlaceholder => _localizer["AddressDialog.StreetPlaceholder"];
    public string AddressFieldHouseNr => _localizer["AddressDialog.HouseNr"];
    public string AddressFieldHouseNrPlaceholder => _localizer["AddressDialog.HouseNrPlaceholder"];
    public string AddressFieldZip => _localizer["AddressDialog.Zip"];
    public string AddressFieldZipPlaceholder => _localizer["AddressDialog.ZipPlaceholder"];
    public string AddressFieldCity => _localizer["AddressDialog.City"];
    public string AddressFieldCityPlaceholder => _localizer["AddressDialog.CityPlaceholder"];
    public string AddressFieldCountry => _localizer["AddressDialog.Country"];
    public string AddressFieldDefaultDeliveryAddress => _localizer["AddressDialog.DefaultDeliveryAddress"];
    public string AddressFieldDefaultInvoiceAddress => _localizer["AddressDialog.DefaultInvoiceAddress"];
    public string CommonSave => _localizer["Common.Save"];
    public string CommonCancel => _localizer["Common.Cancel"];

    #endregion

    #region UI State

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool CanSave =>
        !string.IsNullOrWhiteSpace(Firstname) &&
        !string.IsNullOrWhiteSpace(Lastname) &&
        !IsLoading;

    public bool IsNotLoading => !IsLoading;

    #endregion

    private async Task LoadCustomerAsync()
    {
        if (!_customerId.HasValue) return;

        IsLoading = true;
        try
        {
            var customer = await _customerService.GetCustomerAsync(_customerId.Value);
            if (customer != null)
            {
                // Personal Information
                Firstname = customer.Firstname;
                Lastname = customer.Lastname;

                // Contact Information
                Email = customer.Email ?? string.Empty;
                Phone = customer.Phone ?? string.Empty;
                Website = customer.Website ?? string.Empty;

                // Business Information
                CompanyName = customer.CompanyName ?? string.Empty;
                VatNumber = customer.VatNumber ?? string.Empty;

                // Status and Notes
                CustomerStatus = customer.CustomerStatus;
                Note = customer.Note ?? string.Empty;

                // Addresses
                Addresses.Clear();
                foreach (var address in customer.CustomerAddresses)
                {
                    Addresses.Add(new EditableCustomerAddress
                    {
                        Id = address.Id,
                        Firstname = address.Firstname,
                        Lastname = address.Lastname,
                        CompanyName = address.CompanyName,
                        Street = address.Street,
                        HouseNr = address.HouseNr,
                        Zip = address.Zip,
                        City = address.City,
                        DefaultDeliveryAddress = address.DefaultDeliveryAddress,
                        DefaultInvoiceAddress = address.DefaultInvoiceAddress,
                        CountryId = address.CountryId
                    });
                }
                OnPropertyChanged(nameof(HasAddresses));
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["CustomerEditPage.Error.LoadFailed"], ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new CustomerInputDto
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Email = Email,
                Phone = Phone,
                Website = Website,
                CompanyName = CompanyName,
                VatNumber = VatNumber,
                CustomerStatus = CustomerStatus,
                Note = Note,
                DateEnrollment = IsEditMode ? DateTimeOffset.MinValue : DateTimeOffset.UtcNow,
                CustomerAddresses = Addresses.Select(a => new CustomerAddressListDto
                {
                    Id = a.Id,
                    Firstname = a.Firstname,
                    Lastname = a.Lastname,
                    CompanyName = a.CompanyName,
                    Street = a.Street,
                    HouseNr = a.HouseNr,
                    Zip = a.Zip,
                    City = a.City,
                    DefaultDeliveryAddress = a.DefaultDeliveryAddress,
                    DefaultInvoiceAddress = a.DefaultInvoiceAddress,
                    CountryId = a.CountryId
                }).ToList()
            };

            if (_customerId.HasValue)
            {
                input.Id = _customerId.Value;
                await _customerService.UpdateCustomerAsync(_customerId.Value, input, ct);
            }
            else
            {
                await _customerService.CreateCustomerAsync(input, ct);
            }

            await _navigator.NavigateBackAsync(this);
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["CustomerEditPage.Error.SaveFailed"], ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task CancelAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    #region Address Management

    public void StartAddAddress()
    {
        IsNewAddress = true;
        EditingAddress = new EditableCustomerAddress
        {
            Firstname = Firstname,
            Lastname = Lastname,
            CompanyName = CompanyName,
            // Use CountryId from first existing address as default for new addresses
            CountryId = Addresses.FirstOrDefault()?.CountryId ?? Guid.Empty
        };
        IsAddressDialogOpen = true;
        OnPropertyChanged(nameof(AddressDialogTitle));
    }

    public void StartEditAddress(EditableCustomerAddress address)
    {
        IsNewAddress = false;
        EditingAddress = new EditableCustomerAddress
        {
            Id = address.Id,
            Firstname = address.Firstname,
            Lastname = address.Lastname,
            CompanyName = address.CompanyName,
            Street = address.Street,
            HouseNr = address.HouseNr,
            Zip = address.Zip,
            City = address.City,
            DefaultDeliveryAddress = address.DefaultDeliveryAddress,
            DefaultInvoiceAddress = address.DefaultInvoiceAddress,
            CountryId = address.CountryId,
            OriginalAddress = address
        };
        IsAddressDialogOpen = true;
        OnPropertyChanged(nameof(AddressDialogTitle));
    }

    public void SaveAddress()
    {
        if (EditingAddress == null) return;

        if (IsNewAddress)
        {
            var newAddress = new EditableCustomerAddress
            {
                Id = Guid.Empty,
                Firstname = EditingAddress.Firstname,
                Lastname = EditingAddress.Lastname,
                CompanyName = EditingAddress.CompanyName,
                Street = EditingAddress.Street,
                HouseNr = EditingAddress.HouseNr,
                Zip = EditingAddress.Zip,
                City = EditingAddress.City,
                DefaultDeliveryAddress = EditingAddress.DefaultDeliveryAddress,
                DefaultInvoiceAddress = EditingAddress.DefaultInvoiceAddress,
                CountryId = EditingAddress.CountryId
            };

            if (newAddress.DefaultDeliveryAddress)
            {
                foreach (var addr in Addresses)
                    addr.DefaultDeliveryAddress = false;
            }
            if (newAddress.DefaultInvoiceAddress)
            {
                foreach (var addr in Addresses)
                    addr.DefaultInvoiceAddress = false;
            }

            Addresses.Add(newAddress);
        }
        else if (EditingAddress.OriginalAddress != null)
        {
            var original = EditingAddress.OriginalAddress;

            if (EditingAddress.DefaultDeliveryAddress && !original.DefaultDeliveryAddress)
            {
                foreach (var addr in Addresses)
                    addr.DefaultDeliveryAddress = false;
            }
            if (EditingAddress.DefaultInvoiceAddress && !original.DefaultInvoiceAddress)
            {
                foreach (var addr in Addresses)
                    addr.DefaultInvoiceAddress = false;
            }

            original.Firstname = EditingAddress.Firstname;
            original.Lastname = EditingAddress.Lastname;
            original.CompanyName = EditingAddress.CompanyName;
            original.Street = EditingAddress.Street;
            original.HouseNr = EditingAddress.HouseNr;
            original.Zip = EditingAddress.Zip;
            original.City = EditingAddress.City;
            original.DefaultDeliveryAddress = EditingAddress.DefaultDeliveryAddress;
            original.DefaultInvoiceAddress = EditingAddress.DefaultInvoiceAddress;
            original.CountryId = EditingAddress.CountryId;
        }

        CloseAddressDialog();
        OnPropertyChanged(nameof(HasAddresses));
    }

    public void DeleteAddress(EditableCustomerAddress address)
    {
        Addresses.Remove(address);
        OnPropertyChanged(nameof(HasAddresses));
    }

    public void CloseAddressDialog()
    {
        IsAddressDialogOpen = false;
        EditingAddress = null;
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Update dependent properties
        if (propertyName is nameof(Firstname) or nameof(Lastname) or nameof(IsLoading))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanSave)));
        }

        if (propertyName is nameof(IsLoading))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotLoading)));
        }
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

/// <summary>
/// Represents a customer status option for the ComboBox.
/// </summary>
public record CustomerStatusOption(CustomerStatus Value, string ResourceKey);

/// <summary>
/// Editable address model for the customer edit form.
/// </summary>
public class EditableCustomerAddress : INotifyPropertyChanged
{
    private Guid _id;
    private string _firstname = string.Empty;
    private string _lastname = string.Empty;
    private string _companyName = string.Empty;
    private string _street = string.Empty;
    private string _houseNr = string.Empty;
    private string _zip = string.Empty;
    private string _city = string.Empty;
    private bool _defaultDeliveryAddress;
    private bool _defaultInvoiceAddress;
    private Guid _countryId;

    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Firstname
    {
        get => _firstname;
        set => SetProperty(ref _firstname, value);
    }

    public string Lastname
    {
        get => _lastname;
        set => SetProperty(ref _lastname, value);
    }

    public string CompanyName
    {
        get => _companyName;
        set => SetProperty(ref _companyName, value);
    }

    public string Street
    {
        get => _street;
        set => SetProperty(ref _street, value);
    }

    public string HouseNr
    {
        get => _houseNr;
        set => SetProperty(ref _houseNr, value);
    }

    public string Zip
    {
        get => _zip;
        set => SetProperty(ref _zip, value);
    }

    public string City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }

    public bool DefaultDeliveryAddress
    {
        get => _defaultDeliveryAddress;
        set => SetProperty(ref _defaultDeliveryAddress, value);
    }

    public bool DefaultInvoiceAddress
    {
        get => _defaultInvoiceAddress;
        set => SetProperty(ref _defaultInvoiceAddress, value);
    }

    public Guid CountryId
    {
        get => _countryId;
        set => SetProperty(ref _countryId, value);
    }

    public string FullName => $"{Firstname} {Lastname}".Trim();

    public string FormattedAddress => $"{Street} {HouseNr}, {Zip} {City}".Trim(' ', ',');

    public EditableCustomerAddress? OriginalAddress { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
