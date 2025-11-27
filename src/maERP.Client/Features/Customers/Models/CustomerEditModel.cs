using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Constants;
using maERP.Client.Features.Customers.Services;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Models;

/// <summary>
/// Model for customer edit/create page.
/// Uses INotifyPropertyChanged for form binding.
/// </summary>
public class CustomerEditModel : INotifyPropertyChanged
{
    private readonly ICustomerService _customerService;
    private readonly INavigator _navigator;
    private readonly Guid? _customerId;

    private string _firstname = string.Empty;
    private string _lastname = string.Empty;
    private string _email = string.Empty;
    private string _phone = string.Empty;
    private bool _isLoading;
    private string _errorMessage = string.Empty;

    public CustomerEditModel(
        ICustomerService customerService,
        INavigator navigator,
        CustomerEditData? data = null)
    {
        _customerService = customerService;
        _navigator = navigator;
        _customerId = data?.customerId;

        if (_customerId.HasValue)
        {
            _ = LoadCustomerAsync();
        }
    }

    public bool IsEditMode => _customerId.HasValue;
    public string Title => IsEditMode ? "Edit Customer" : "New Customer";

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

    private async Task LoadCustomerAsync()
    {
        if (!_customerId.HasValue) return;

        IsLoading = true;
        try
        {
            var customer = await _customerService.GetCustomerAsync(_customerId.Value);
            if (customer != null)
            {
                Firstname = customer.Firstname;
                Lastname = customer.Lastname;
                Email = customer.Email ?? string.Empty;
                // Phone = customer.Phone ?? string.Empty; // Add if available in DTO
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load customer: {ex.Message}";
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
                // Add other fields as needed
            };

            if (_customerId.HasValue)
            {
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
            ErrorMessage = $"Failed to save customer: {ex.Message}";
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
        if (propertyName is nameof(Firstname) or nameof(Lastname) or nameof(IsLoading))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanSave)));
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
