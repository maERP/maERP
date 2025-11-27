using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Features.Customers.Services;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Enums;

namespace maERP.Client.Features.Customers.Models;

/// <summary>
/// Model for customer edit/create page.
/// Uses INotifyPropertyChanged for form binding.
/// </summary>
public class CustomerEditModel : INotifyPropertyChanged
{
    private readonly ICustomerService _customerService;
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

    // UI State
    private bool _isLoading;
    private string _errorMessage = string.Empty;

    public CustomerEditModel(
        ICustomerService customerService,
        INavigator navigator,
        IStringLocalizer localizer,
        CustomerEditData? data = null)
    {
        _customerService = customerService;
        _navigator = navigator;
        _localizer = localizer;
        _customerId = data?.customerId;

        if (_customerId.HasValue)
        {
            _ = LoadCustomerAsync();
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
                DateEnrollment = IsEditMode ? DateTimeOffset.MinValue : DateTimeOffset.UtcNow
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
