using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;
using maERP.UI.Features.Customers.Validators;
using maERP.UI.Services;
using maERP.UI.Shared.Validation;

namespace maERP.UI.Features.Customers.ViewModels;

public partial class CustomerInputViewModel : FluentValidationViewModelBase, ICustomerInputModel
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly CustomerClientValidator _validator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid customerId;

    [ObservableProperty]
    private string firstname = string.Empty;

    [ObservableProperty]
    private string lastname = string.Empty;

    [ObservableProperty]
    private string companyName = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string phone = string.Empty;

    [ObservableProperty]
    private string website = string.Empty;

    [ObservableProperty]
    private string vatNumber = string.Empty;

    [ObservableProperty]
    private string note = string.Empty;

    [ObservableProperty]
    private CustomerStatus customerStatus = CustomerStatus.Active;

    [ObservableProperty]
    private DateTimeOffset dateEnrollment = DateTimeOffset.Now;

    [ObservableProperty]
    private ObservableCollection<CustomerAddressListDto> customerAddresses = new();

    [ObservableProperty]
    private CustomerAddressListDto? selectedAddress;

    [ObservableProperty]
    private bool isAddingAddress;

    [ObservableProperty]
    private bool isEditingAddress;

    // New address form fields
    [ObservableProperty]
    private string newAddressFirstname = string.Empty;

    [ObservableProperty]
    private string newAddressLastname = string.Empty;

    [ObservableProperty]
    private string newAddressCompanyName = string.Empty;

    [ObservableProperty]
    private string newAddressStreet = string.Empty;

    [ObservableProperty]
    private string newAddressHouseNr = string.Empty;

    [ObservableProperty]
    private string newAddressZip = string.Empty;

    [ObservableProperty]
    private string newAddressCity = string.Empty;

    [ObservableProperty]
    private bool newAddressDefaultDelivery;

    [ObservableProperty]
    private bool newAddressDefaultInvoice;

    [ObservableProperty]
    private int newAddressCountryId = 1; // Default to first country

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => CustomerId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"Kunde #{CustomerId} bearbeiten" : "Neuen Kunden erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    // Validation Error Properties for XAML Binding
    public string? FirstnameError => GetFirstErrorMessage(nameof(Firstname));
    public string? LastnameError => GetFirstErrorMessage(nameof(Lastname));
    public string? EmailError => GetFirstErrorMessage(nameof(Email));
    public string? PhoneError => GetFirstErrorMessage(nameof(Phone));
    public string? WebsiteError => GetFirstErrorMessage(nameof(Website));

    // Available options for dropdowns
    public List<CustomerStatus> AvailableStatuses { get; } = Enum.GetValues<CustomerStatus>().ToList();

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToCustomerDetail { get; set; }

    public CustomerInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _validator = new CustomerClientValidator();
    }

    /// <summary>
    /// Gibt den FluentValidator für dieses ViewModel zurück.
    /// Erforderlich von FluentValidationViewModelBase.
    /// </summary>
    protected override IValidator GetValidator() => _validator;

    // Property-Change Validierung für Echtzeit-Feedback
    partial void OnFirstnameChanged(string value) => ValidateProperty(nameof(Firstname));
    partial void OnLastnameChanged(string value) => ValidateProperty(nameof(Lastname));
    partial void OnEmailChanged(string value) => ValidateProperty(nameof(Email));
    partial void OnPhoneChanged(string value) => ValidateProperty(nameof(Phone));
    partial void OnWebsiteChanged(string value) => ValidateProperty(nameof(Website));

    public async Task InitializeAsync(Guid customerId = default)
    {
        CustomerId = customerId;

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
        if (CustomerId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<CustomerDetailDto>($"customers/{CustomerId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var customer = result.Data;

                // Map customer data to form fields
                Firstname = customer.Firstname;
                Lastname = customer.Lastname;
                CompanyName = customer.CompanyName;
                Email = customer.Email;
                Phone = customer.Phone;
                Website = customer.Website;
                VatNumber = customer.VatNumber;
                Note = customer.Note;
                CustomerStatus = customer.CustomerStatus;
                DateEnrollment = customer.DateEnrollment;

                // Load addresses
                CustomerAddresses.Clear();
                if (customer.CustomerAddresses != null)
                {
                    foreach (var address in customer.CustomerAddresses)
                    {
                        CustomerAddresses.Add(address);
                    }
                }

                _debugService.LogInfo($"Loaded customer {CustomerId} for editing successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Kunden {CustomerId}";
                _debugService.LogError($"Failed to load customer {CustomerId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Kunden: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading customer {CustomerId}");
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
            var customerDto = new CustomerInputDto
            {
                Id = CustomerId,
                Firstname = Firstname,
                Lastname = Lastname,
                CompanyName = CompanyName,
                Email = Email,
                Phone = Phone,
                Website = Website,
                VatNumber = VatNumber,
                Note = Note,
                CustomerStatus = CustomerStatus,
                DateEnrollment = DateEnrollment,
                CustomerAddresses = CustomerAddresses.ToList()
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<CustomerInputDto, Guid>($"customers/{CustomerId}", customerDto)
                : await _httpService.PostAsync<CustomerInputDto, Guid>("customers", customerDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Customer {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToCustomerDetail != null)
                {
                    await NavigateToCustomerDetail(CustomerId);
                }
                else if (!IsEditMode && NavigateToCustomerDetail != null)
                {
                    var createdCustomerId = result.Data;
                    if (createdCustomerId != Guid.Empty)
                    {
                        await NavigateToCustomerDetail(createdCustomerId);
                    }
                    else
                    {
                        GoBack();
                    }
                }
                else
                {
                    GoBack();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Speichern" : "Erstellen")} des Kunden";
                _debugService.LogError($"Failed to save customer: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving customer");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoBack();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    private void ClearForm()
    {
        Firstname = string.Empty;
        Lastname = string.Empty;
        CompanyName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Website = string.Empty;
        VatNumber = string.Empty;
        Note = string.Empty;
        CustomerStatus = CustomerStatus.Active;
        DateEnrollment = DateTimeOffset.Now;
        CustomerAddresses.Clear();
        ErrorMessage = string.Empty;

        ClearErrors();
    }

    private bool ValidateForm()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Eingabefehler";
            return false;
        }
        return true;
    }

    // Address management commands
    [RelayCommand]
    private void StartAddingAddress()
    {
        IsAddingAddress = true;
        IsEditingAddress = false;
        ClearAddressForm();

        // Pre-fill with customer data if available
        if (!string.IsNullOrEmpty(Firstname) || !string.IsNullOrEmpty(Lastname))
        {
            NewAddressFirstname = Firstname;
            NewAddressLastname = Lastname;
        }
        if (!string.IsNullOrEmpty(CompanyName))
        {
            NewAddressCompanyName = CompanyName;
        }
    }

    [RelayCommand]
    private void StartEditingAddress(CustomerAddressListDto? address)
    {
        if (address == null) return;

        SelectedAddress = address;
        IsEditingAddress = true;
        IsAddingAddress = true; // Show the form

        // Fill form with existing address data
        NewAddressFirstname = address.Firstname;
        NewAddressLastname = address.Lastname;
        NewAddressCompanyName = address.CompanyName;
        NewAddressStreet = address.Street;
        NewAddressHouseNr = address.HouseNr;
        NewAddressZip = address.Zip;
        NewAddressCity = address.City;
        NewAddressDefaultDelivery = address.DefaultDeliveryAddress;
        NewAddressDefaultInvoice = address.DefaultInvoiceAddress;
    }

    [RelayCommand]
    private void SaveAddress()
    {
        if (IsEditingAddress && SelectedAddress != null)
        {
            // Update existing address
            SelectedAddress.Firstname = NewAddressFirstname;
            SelectedAddress.Lastname = NewAddressLastname;
            SelectedAddress.CompanyName = NewAddressCompanyName;
            SelectedAddress.Street = NewAddressStreet;
            SelectedAddress.HouseNr = NewAddressHouseNr;
            SelectedAddress.Zip = NewAddressZip;
            SelectedAddress.City = NewAddressCity;
            SelectedAddress.DefaultDeliveryAddress = NewAddressDefaultDelivery;
            SelectedAddress.DefaultInvoiceAddress = NewAddressDefaultInvoice;

            // Ensure only one default delivery and invoice address
            if (NewAddressDefaultDelivery)
            {
                foreach (var addr in CustomerAddresses.Where(a => a != SelectedAddress))
                {
                    addr.DefaultDeliveryAddress = false;
                }
            }
            if (NewAddressDefaultInvoice)
            {
                foreach (var addr in CustomerAddresses.Where(a => a != SelectedAddress))
                {
                    addr.DefaultInvoiceAddress = false;
                }
            }
        }
        else if (IsAddingAddress)
        {
            // Create new address
            var newAddress = new CustomerAddressListDto
            {
                Id = Guid.Empty, // Will be set by server
                Firstname = NewAddressFirstname,
                Lastname = NewAddressLastname,
                CompanyName = NewAddressCompanyName,
                Street = NewAddressStreet,
                HouseNr = NewAddressHouseNr,
                Zip = NewAddressZip,
                City = NewAddressCity,
                DefaultDeliveryAddress = NewAddressDefaultDelivery,
                DefaultInvoiceAddress = NewAddressDefaultInvoice
            };

            // Ensure only one default delivery and invoice address
            if (NewAddressDefaultDelivery)
            {
                foreach (var addr in CustomerAddresses)
                {
                    addr.DefaultDeliveryAddress = false;
                }
            }
            if (NewAddressDefaultInvoice)
            {
                foreach (var addr in CustomerAddresses)
                {
                    addr.DefaultInvoiceAddress = false;
                }
            }

            CustomerAddresses.Add(newAddress);
        }

        CancelAddressEdit();
    }

    [RelayCommand]
    private void CancelAddressEdit()
    {
        IsAddingAddress = false;
        IsEditingAddress = false;
        SelectedAddress = null;
        ClearAddressForm();
    }

    [RelayCommand]
    private void DeleteAddress(CustomerAddressListDto? address)
    {
        if (address == null) return;

        CustomerAddresses.Remove(address);

        if (SelectedAddress == address)
        {
            CancelAddressEdit();
        }
    }

    private void ClearAddressForm()
    {
        NewAddressFirstname = string.Empty;
        NewAddressLastname = string.Empty;
        NewAddressCompanyName = string.Empty;
        NewAddressStreet = string.Empty;
        NewAddressHouseNr = string.Empty;
        NewAddressZip = string.Empty;
        NewAddressCity = string.Empty;
        NewAddressDefaultDelivery = false;
        NewAddressDefaultInvoice = false;
        NewAddressCountryId = 1;
    }
}
