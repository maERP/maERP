using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Repositories;

public class OrderImportRepository : IOrderImportRepository
{
    private readonly ILogger<ProductImportRepository> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrderImportRepository(
        ILogger<ProductImportRepository> logger,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportOrder importOrder)
    {
        var existingOrder = await _orderRepository.GetByRemoteOrderIdAsync(salesChannelId, importOrder.RemoteOrderId);

        if (existingOrder == null)
        {
            _logger.LogInformation("Order {0} does not exist, creating Order", importOrder.RemoteOrderId);
            
            // try to find customer in sales channel
            var customer = await _customerRepository.GetCustomerByRemoteCustomerIdAsync(salesChannelId, importOrder.RemoteCustomerId);

            // when not found, try to find via email
            if (customer == null && importOrder.Customer != null && !string.IsNullOrEmpty(importOrder.Customer.Email))
            {
                customer = await _customerRepository.GetCustomerByEmailAsync(importOrder.Customer.Email);

                // when found, add to CustomerSalesChannel
                if (customer != null)
                {
                    await _customerRepository.AddCustomerToSalesChannelAsync(customer.Id, salesChannelId, importOrder.RemoteCustomerId);
                    _logger.LogInformation("CustomerSalesChannel added for Customer {0} ", customer.Id);
                }
            }
            
            // when still not found, create new customer
            if(customer == null)
            {
                var newCustomer = new Customer
                {
                    Email = importOrder.Customer?.Email ?? string.Empty,
                    Firstname = importOrder.Customer?.Firstname ?? string.Empty,
                    Lastname = importOrder.Customer?.Lastname ?? string.Empty,
                    CompanyName = importOrder.Customer?.CompanyName ?? string.Empty,
                    Phone = importOrder.Customer?.Phone ?? string.Empty,
                    Website = importOrder.Customer?.Website ?? string.Empty,
                    VatNumber = importOrder.Customer?.VatNumber ?? string.Empty,
                    Note = importOrder.Customer?.Note ?? string.Empty,
                    CustomerStatus = importOrder.Customer?.CustomerStatus ?? CustomerStatus.Active,
                    DateEnrollment = importOrder.Customer?.DateEnrollment ?? DateTime.Now,
                };

                await _customerRepository.CreateAsync(newCustomer);
                _logger.LogInformation("Customer {0} created", importOrder.Customer?.Email);
                customer = newCustomer;
                
                await _customerRepository.AddCustomerToSalesChannelAsync(newCustomer.Id, salesChannelId, importOrder.RemoteCustomerId);
                _logger.LogInformation("CustomerSalesChannel added for Customer {0} ", customer.Id);
            }

            int billingAddressId = 0;
            int shippingAddressId = 0; 
            var customerAddresses = await _customerRepository.GetCustomerAddressByCustomerIdAsync(customer.Id);
            
            foreach(var address in customerAddresses)
            {
                if (address.Firstname == importOrder.BillingAddress.Firstname &&
                    address.Lastname == importOrder.BillingAddress.Lastname &&
                    address.CompanyName == importOrder.BillingAddress.CompanyName &&
                    address.Street == importOrder.BillingAddress.Street &&
                    address.City == importOrder.BillingAddress.City &&
                    address.Zip == importOrder.BillingAddress.Zip)
                {
                    billingAddressId = address.Id;
                }
                
                if (address.Firstname == importOrder.ShippingAddress.Firstname &&
                    address.Lastname == importOrder.ShippingAddress.Lastname &&
                    address.CompanyName == importOrder.ShippingAddress.CompanyName &&
                    address.Street == importOrder.ShippingAddress.Street &&
                    address.City == importOrder.ShippingAddress.City &&
                    address.Zip == importOrder.ShippingAddress.Zip)
                {
                    shippingAddressId = address.Id;
                }
                
                if(billingAddressId > 0 && shippingAddressId > 0)
                {
                    break;
                }
            }

            if (billingAddressId == 0)
            {
                var newAddress = new CustomerAddress
                {
                    Firstname = importOrder.BillingAddress.Firstname,
                    Lastname = importOrder.BillingAddress.Lastname,
                    CompanyName = importOrder.BillingAddress.CompanyName,
                    Street = importOrder.BillingAddress.Street,
                    City = importOrder.BillingAddress.City,
                    Zip = importOrder.BillingAddress.Zip,
                };
                
                await _customerRepository.AddCustomerAddressAsync(newAddress);
            }
            
            if(shippingAddressId > 0 && shippingAddressId != billingAddressId)
            {
                var newAddress = new CustomerAddress
                {
                    Firstname = importOrder.ShippingAddress.Firstname,
                    Lastname = importOrder.ShippingAddress.Lastname,
                    CompanyName = importOrder.ShippingAddress.CompanyName,
                    Street = importOrder.ShippingAddress.Street,
                    City = importOrder.ShippingAddress.City,
                    Zip = importOrder.ShippingAddress.Zip,
                };
                
                await _customerRepository.AddCustomerAddressAsync(newAddress);
            }

            var newOrder = new Order
            {
                SalesChannelId = salesChannelId,
                RemoteOrderId = importOrder.RemoteOrderId,
                CustomerId = customer.Id,
                Status = importOrder.Status,
                
                PaymentMethod = importOrder.PaymentMethod,
                PaymentStatus = importOrder.PaymentStatus,
                PaymentProvider = importOrder.PaymentProvider,
                PaymentTransactionId = importOrder.PaymentTransactionId,

                ShippingMethod = importOrder.ShippingMethod,
                ShippingStatus = importOrder.ShippingStatus,
                ShippingProvider = importOrder.ShippingProvider,
                ShippingTrackingId = importOrder.ShippingTrackingId,

                Subtotal = importOrder.Subtotal,
                ShippingCost = importOrder.ShippingCost,
                TotalTax = importOrder.TotalTax,
                Total = importOrder.Total,
                            
                InvoiceAddressFirstName = importOrder.BillingAddress.Firstname,
                InvoiceAddressLastName = importOrder.BillingAddress.Lastname,
                InvoiceAddressCompanyName = importOrder.BillingAddress.CompanyName,
                InvoiceAddressStreet = importOrder.BillingAddress.Street,
                InvoiceAddressCity = importOrder.BillingAddress.City,
                InvoiceAddressZip = importOrder.BillingAddress.Zip,
                InvoiceAddressCountry = importOrder.BillingAddress.Country,
                
                DeliveryAddressFirstName = importOrder.ShippingAddress.Firstname,
                DeliveryAddressLastName = importOrder.ShippingAddress.Lastname,
                DeliveryAddressCompanyName = importOrder.ShippingAddress.CompanyName,
                DeliveryAddressStreet = importOrder.ShippingAddress.Street,
                DeliveryAddressCity = importOrder.ShippingAddress.City,
                DeliverAddressZip = importOrder.ShippingAddress.Zip
            };

            await _orderRepository.CreateAsync(newOrder);
            _logger.LogInformation("Order {0} created", importOrder.RemoteOrderId);
        }
        else
        {
            _logger.LogInformation("Order {0} already exists, check for SalesChannel", existingOrder.RemoteOrderId);
            bool somethingChanged = false;

            if (somethingChanged)
            {
                await _orderRepository.UpdateAsync(existingOrder);
                _logger.LogInformation("Order {0} updated", importOrder.RemoteOrderId);
            }
        }
    }
}