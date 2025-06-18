using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Customers.ViewModels;

public partial class CustomerDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private CustomerDetailDto? customer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int customerId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Customer != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditCustomer { get; set; }

    // Computed properties for better display
    public bool HasCompanyName => Customer != null && !string.IsNullOrEmpty(Customer.CompanyName);
    public bool HasEmail => Customer != null && !string.IsNullOrEmpty(Customer.Email);
    public bool HasPhone => Customer != null && !string.IsNullOrEmpty(Customer.Phone);
    public bool HasWebsite => Customer != null && !string.IsNullOrEmpty(Customer.Website);
    public bool HasVatNumber => Customer != null && !string.IsNullOrEmpty(Customer.VatNumber);
    public bool HasNote => Customer != null && !string.IsNullOrEmpty(Customer.Note);
    public bool HasAddresses => Customer?.CustomerAddresses?.Any() == true;

    // Address-related properties
    public CustomerAddressListDto? DefaultDeliveryAddress => Customer?.CustomerAddresses?.FirstOrDefault(a => a.DefaultDeliveryAddress);
    public CustomerAddressListDto? DefaultInvoiceAddress => Customer?.CustomerAddresses?.FirstOrDefault(a => a.DefaultInvoiceAddress);
    public bool HasDefaultDeliveryAddress => DefaultDeliveryAddress != null;
    public bool HasDefaultInvoiceAddress => DefaultInvoiceAddress != null;

    public string GetFormattedAddress(CustomerAddressListDto address)
    {
        if (address == null) return string.Empty;
        
        var addressLines = new List<string>();
        
        if (!string.IsNullOrEmpty(address.CompanyName))
            addressLines.Add(address.CompanyName);
        
        if (!string.IsNullOrEmpty(address.Firstname) || !string.IsNullOrEmpty(address.Lastname))
            addressLines.Add($"{address.Firstname} {address.Lastname}".Trim());
        
        if (!string.IsNullOrEmpty(address.Street) || !string.IsNullOrEmpty(address.HouseNr))
            addressLines.Add($"{address.Street} {address.HouseNr}".Trim());
        
        if (!string.IsNullOrEmpty(address.Zip) || !string.IsNullOrEmpty(address.City))
            addressLines.Add($"{address.Zip} {address.City}".Trim());
        
        return string.Join("\n", addressLines);
    }

    public string DefaultDeliveryAddressFormatted => DefaultDeliveryAddress != null 
        ? GetFormattedAddress(DefaultDeliveryAddress) 
        : "Keine Standard-Lieferadresse";

    public string DefaultInvoiceAddressFormatted => DefaultInvoiceAddress != null 
        ? GetFormattedAddress(DefaultInvoiceAddress) 
        : "Keine Standard-Rechnungsadresse";

    public CustomerDetailViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int customerId)
    {
        CustomerId = customerId;
        await LoadCustomerAsync();
    }

    [RelayCommand]
    private async Task LoadCustomerAsync()
    {
        if (CustomerId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<CustomerDetailDto>($"customers/{CustomerId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Customer = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Customer = result.Data;
                OnPropertyChanged(nameof(HasCompanyName));
                OnPropertyChanged(nameof(HasEmail));
                OnPropertyChanged(nameof(HasPhone));
                OnPropertyChanged(nameof(HasWebsite));
                OnPropertyChanged(nameof(HasVatNumber));
                OnPropertyChanged(nameof(HasNote));
                OnPropertyChanged(nameof(HasAddresses));
                OnPropertyChanged(nameof(DefaultDeliveryAddress));
                OnPropertyChanged(nameof(DefaultInvoiceAddress));
                OnPropertyChanged(nameof(HasDefaultDeliveryAddress));
                OnPropertyChanged(nameof(HasDefaultInvoiceAddress));
                OnPropertyChanged(nameof(DefaultDeliveryAddressFormatted));
                OnPropertyChanged(nameof(DefaultInvoiceAddressFormatted));
                _debugService.LogInfo($"Loaded customer {CustomerId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Kunden {CustomerId}";
                Customer = null;
                _debugService.LogError($"Failed to load customer {CustomerId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Kunden: {ex.Message}";
            Customer = null;
            _debugService.LogError(ex, $"Exception loading customer {CustomerId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadCustomerAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditCustomer()
    {
        if (Customer == null || NavigateToEditCustomer == null) return;
        
        await NavigateToEditCustomer(Customer.Id);
    }

    [RelayCommand]
    private void SendEmail()
    {
        if (Customer == null || string.IsNullOrEmpty(Customer.Email)) return;

        try
        {
            var emailUri = $"mailto:{Customer.Email}";
            OpenUrl(emailUri);
            _debugService.LogInfo($"Opening email client for: {Customer.Email}");
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Failed to open email client");
        }
    }

    [RelayCommand]
    private void CallPhone()
    {
        if (Customer == null || string.IsNullOrEmpty(Customer.Phone)) return;

        try
        {
            var phoneUri = $"tel:{Customer.Phone}";
            OpenUrl(phoneUri);
            _debugService.LogInfo($"Opening phone app for: {Customer.Phone}");
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Failed to open phone app");
        }
    }

    [RelayCommand]
    private void OpenWebsite()
    {
        if (Customer == null || string.IsNullOrEmpty(Customer.Website)) return;

        try
        {
            var website = Customer.Website;
            if (!website.StartsWith("http://") && !website.StartsWith("https://"))
            {
                website = "https://" + website;
            }
            OpenUrl(website);
            _debugService.LogInfo($"Opening website: {website}");
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Failed to open website");
        }
    }

    private void OpenUrl(string url)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, $"Failed to open URL {url}");
        }
    }
}