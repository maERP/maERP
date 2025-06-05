using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class CustomerDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

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

    public CustomerDetailViewModel(IHttpService httpService)
    {
        _httpService = httpService;
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
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
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
                System.Diagnostics.Debug.WriteLine($"Loaded customer {CustomerId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Kunden {CustomerId}";
                Customer = null;
                System.Diagnostics.Debug.WriteLine($"Failed to load customer {CustomerId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Kunden: {ex.Message}";
            Customer = null;
            System.Diagnostics.Debug.WriteLine($"Exception loading customer {CustomerId}: {ex}");
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
}