using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Repositories;

public class CustomerImportRepository : ICustomerImportRepository
{
    private readonly ILogger<CustomerImportRepository> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICountryRepository _countryRepository;

    public CustomerImportRepository(
        ILogger<CustomerImportRepository> logger,
        ICustomerRepository customerRepository,
        ICountryRepository countryRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _countryRepository = countryRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportCustomer importCustomer)
    {
        // Überprüfen, ob Kunde bereits existiert (nach Remote-ID suchen)
        var existingCustomer = await _customerRepository.GetCustomerByRemoteCustomerIdAsync(salesChannel.Id, importCustomer.RemoteCustomerId);

        // Wenn nicht nach Remote-ID gefunden, nach E-Mail suchen
        if (existingCustomer == null && !string.IsNullOrEmpty(importCustomer.Email))
        {
            existingCustomer = await _customerRepository.GetCustomerByEmailAsync(importCustomer.Email);
            
            // Wenn nach E-Mail gefunden, Verknüpfung mit SalesChannel herstellen
            if (existingCustomer != null)
            {
                await _customerRepository.AddCustomerToSalesChannelAsync(existingCustomer.Id, salesChannel.Id, importCustomer.RemoteCustomerId);
                _logger.LogInformation($"CustomerSalesChannel hinzugefügt für Kunden {existingCustomer.Id}");
            }
        }
        
        // Wenn Kunde nicht existiert, neu anlegen
        if (existingCustomer == null)
        {
            var newCustomer = new Customer
            {
                Email = importCustomer.Email ?? string.Empty,
                Firstname = importCustomer.Firstname ?? string.Empty,
                Lastname = importCustomer.Lastname ?? string.Empty,
                CompanyName = importCustomer.CompanyName ?? string.Empty,
                Phone = importCustomer.Phone ?? string.Empty,
                Website = importCustomer.Website ?? string.Empty,
                VatNumber = importCustomer.VatNumber ?? string.Empty,
                Note = importCustomer.Note ?? string.Empty,
                CustomerStatus = importCustomer.CustomerStatus != 0 ? importCustomer.CustomerStatus : CustomerStatus.Active,
                DateEnrollment = importCustomer.DateEnrollment != DateTime.MinValue ? importCustomer.DateEnrollment : DateTime.UtcNow
            };

            await _customerRepository.CreateAsync(newCustomer);
            _logger.LogInformation($"Kunde {importCustomer.Email} erstellt");
            
            // Verknüpfung mit SalesChannel herstellen
            await _customerRepository.AddCustomerToSalesChannelAsync(newCustomer.Id, salesChannel.Id, importCustomer.RemoteCustomerId);
            _logger.LogInformation($"CustomerSalesChannel hinzugefügt für Kunden {newCustomer.Id}");
            
            existingCustomer = newCustomer;
        }
        else
        {
            // Bestehenden Kunden aktualisieren
            existingCustomer.Email = !string.IsNullOrEmpty(importCustomer.Email) ? importCustomer.Email : existingCustomer.Email;
            existingCustomer.Firstname = !string.IsNullOrEmpty(importCustomer.Firstname) ? importCustomer.Firstname : existingCustomer.Firstname;
            existingCustomer.Lastname = !string.IsNullOrEmpty(importCustomer.Lastname) ? importCustomer.Lastname : existingCustomer.Lastname;
            existingCustomer.CompanyName = !string.IsNullOrEmpty(importCustomer.CompanyName) ? importCustomer.CompanyName : existingCustomer.CompanyName;
            existingCustomer.Phone = !string.IsNullOrEmpty(importCustomer.Phone) ? importCustomer.Phone : existingCustomer.Phone;
            
            await _customerRepository.UpdateAsync(existingCustomer);
            _logger.LogInformation($"Kunde {existingCustomer.Id} aktualisiert");
        }
        
        // Adressen verarbeiten
        if (importCustomer.BillingAddress != null)
        {
            await ProcessAddress(existingCustomer, importCustomer.BillingAddress);
        }
        
        if (importCustomer.ShippingAddress != null && 
            (importCustomer.BillingAddress == null || 
             !AreAddressesEqual(importCustomer.BillingAddress, importCustomer.ShippingAddress)))
        {
            await ProcessAddress(existingCustomer, importCustomer.ShippingAddress);
        }
    }
    
    private async Task ProcessAddress(Customer customer, SalesChannelImportCustomerAddress address)
    {
        // Land aus ISO-Code ermitteln
        Country country = await _countryRepository.GetCountryByString(address.Country);
        if (country == null)
        {
            _logger.LogWarning($"Land mit ISO-Code {address.Country} nicht gefunden");
            return;
        }
        
        // Prüfen, ob Adresse bereits existiert
        var existingAddresses = await _customerRepository.GetCustomerAddressByCustomerIdAsync(customer.Id);
        bool addressExists = existingAddresses.Any(a => 
            a.Street == address.Street && 
            a.City == address.City && 
            a.Zip == address.Zip && 
            a.CountryId == country.Id);
        
        if (!addressExists)
        {
            var newAddress = new CustomerAddress
            {
                CustomerId = customer.Id,
                Firstname = address.Firstname,
                Lastname = address.Lastname,
                CompanyName = address.CompanyName,
                Street = address.Street,
                City = address.City,
                Zip = address.Zip,
                CountryId = country.Id
            };
            
            await _customerRepository.AddCustomerAddressAsync(newAddress);
            _logger.LogInformation($"Neue Adresse für Kunden {customer.Id} hinzugefügt");
        }
    }
    
    private bool AreAddressesEqual(SalesChannelImportCustomerAddress address1, SalesChannelImportCustomerAddress address2)
    {
        return address1.Street == address2.Street &&
               address1.City == address2.City &&
               address1.Zip == address2.Zip &&
               address1.Country == address2.Country;
    }
} 