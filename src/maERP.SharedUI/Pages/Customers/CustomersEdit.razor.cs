using System.Net.Http.Json;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required CustomerInputValidator Validator { get; set; }
    
    [Inject]
    public required IDialogService DialogService { get; set; }

    [Parameter]
    public int customerId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public CustomerInputDto Customer = new();

    protected override async Task OnInitializedAsync()
    {
        if (customerId == 0)
        {
            Title = "Kunde hinzufügen";
            // Setze Standardwerte für neuen Kunden
            Customer.DateEnrollment = DateTime.Today;
        }
        else
        {
            Title = "Kunde bearbeiten";
            
            var result = await HttpService.GetAsync<Result<CustomerInputDto>>($"/api/v1/Customers/{customerId}");
            
            if (result != null && result.Succeeded)
            {
                Customer = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (customerId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/Customers", Customer);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/Customers/{customerId}", Customer);
        }

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Kunde gespeichert", Severity.Success);
                NavigateToList();
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add("SERVER: " + errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("Kunde konnte nicht gespeichert werden", Severity.Error);
        }
    }

    protected async Task OnValidSubmit()
    {
        if (Form is not null)
        {
            await Form.Validate();
            
            if (Form.IsValid)
            {
                await Save();
            }
        }
    }
    
    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Customers");
    }
    
    // Adressenverwaltung
    protected async Task AddNewAddress()
    {
        var parameters = new DialogParameters
        {
            ["Address"] = new CustomerAddressListDto(),
            ["IsNew"] = true
        };
        
        var dialogInstance = await DialogService.ShowAsync<AddressDialog>("Adresse hinzufügen", parameters);
        var dialogResult = await dialogInstance.Result;
        
        if (dialogResult != null && !dialogResult.Canceled)
        {
            var newAddress = dialogResult.Data as CustomerAddressListDto;
            if (newAddress != null)
            {
                Customer.CustomerAddresses.Add(newAddress);
                StateHasChanged();
            }
        }
    }
    
    protected async Task EditAddress(CustomerAddressListDto address)
    {
        var parameters = new DialogParameters
        {
            ["Address"] = address,
            ["IsNew"] = false
        };
        
        var dialogInstance = await DialogService.ShowAsync<AddressDialog>("Adresse bearbeiten", parameters);
        var dialogResult = await dialogInstance.Result;
        
        if (dialogResult != null && !dialogResult.Canceled)
        {
            var updatedAddress = dialogResult.Data as CustomerAddressListDto;
            if (updatedAddress != null)
            {
                var index = Customer.CustomerAddresses.FindIndex(a => a.Id == address.Id);
                if (index >= 0)
                {
                    Customer.CustomerAddresses[index] = updatedAddress;
                }
                
                StateHasChanged();
            }
        }
    }
    
    protected async Task DeleteAddress(CustomerAddressListDto address)
    {
        var parameters = new DialogParameters
        {
            ["ContentText"] = "Sind Sie sicher, dass Sie diese Adresse löschen möchten? Dieser Vorgang kann nicht rückgängig gemacht werden."
        };
        
        var dialogInstance = await DialogService.ShowAsync<ConfirmDialog>("Adresse löschen", parameters);
        var dialogResult = await dialogInstance.Result;
        
        if (dialogResult != null && !dialogResult.Canceled && dialogResult.Data is bool isConfirmed && isConfirmed)
        {
            Customer.CustomerAddresses.Remove(address);
            StateHasChanged();
        }
    }
}