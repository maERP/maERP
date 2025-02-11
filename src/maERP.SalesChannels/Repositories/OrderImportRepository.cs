using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Repositories;

public class OrderImportRepository : IOrderImportRepository
{
    private readonly ILogger<ProductImportRepository> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IProductRepository _productRepository;

    public OrderImportRepository(
        ILogger<ProductImportRepository> logger,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        ICountryRepository countryRepository,
        IProductRepository productRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _countryRepository = countryRepository;
        _productRepository = productRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportOrder importOrder)
    {
        var existingOrder = await _orderRepository.GetByRemoteOrderIdAsync(salesChannel.Id, importOrder.RemoteOrderId);

        if (existingOrder == null)
        {
            _logger.LogInformation("Order {0}: does not exist, create order...", importOrder.RemoteOrderId);
            
            // try to find customer in sales channel
            var customer = await _customerRepository.GetCustomerByRemoteCustomerIdAsync(salesChannel.Id, importOrder.RemoteCustomerId);

            // when not found, try to find via email
            if (customer == null && importOrder.Customer != null && !string.IsNullOrEmpty(importOrder.Customer.Email))
            {
                customer = await _customerRepository.GetCustomerByEmailAsync(importOrder.Customer.Email);

                // when found, add to CustomerSalesChannel
                if (customer != null)
                {
                    await _customerRepository.AddCustomerToSalesChannelAsync(customer.Id, salesChannel.Id, importOrder.RemoteCustomerId);
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
                    DateEnrollment = importOrder.Customer?.DateEnrollment ?? DateTime.UtcNow,
                };

                await _customerRepository.CreateAsync(newCustomer);
                _logger.LogInformation("Customer {0} created", importOrder.Customer?.Email);
                customer = newCustomer;
                
                await _customerRepository.AddCustomerToSalesChannelAsync(newCustomer.Id, salesChannel.Id, importOrder.RemoteCustomerId);
                _logger.LogInformation("CustomerSalesChannel added for Customer {0} ", customer.Id);
            }

            int billingAddressId = 0;
            int shippingAddressId = 0; 
            var customerAddresses = await _customerRepository.GetCustomerAddressByCustomerIdAsync(customer.Id);

            Country? billingAddressCountry = await MapCountryFromStringAsync(importOrder.BillingAddress.Country);
            Country? shippingAddressCountry = await MapCountryFromStringAsync(importOrder.ShippingAddress.Country);

            if(billingAddressCountry == null)
            {
                _logger.LogError("Order {0}: Cannot import, country {1} not found", importOrder.RemoteOrderId, importOrder.BillingAddress.Country);
                return;
            }

            if (shippingAddressCountry == null)
            {
                _logger.LogError("Order {0}: Cannot import, country {1} not found", importOrder.RemoteOrderId, importOrder.ShippingAddress.Country);
                return;
            }

            foreach (var address in customerAddresses)
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
                    Customer = customer,
                    CustomerId = customer.Id,
                    Firstname = importOrder.BillingAddress.Firstname,
                    Lastname = importOrder.BillingAddress.Lastname,
                    CompanyName = importOrder.BillingAddress.CompanyName,
                    Street = importOrder.BillingAddress.Street,
                    City = importOrder.BillingAddress.City,
                    Zip = importOrder.BillingAddress.Zip,
                    Country = billingAddressCountry,
                    CountryId = billingAddressCountry.Id
                };
                
                await _customerRepository.AddCustomerAddressAsync(newAddress);
            }
            
            if(shippingAddressId > 0 && shippingAddressId != billingAddressId)
            {
                var newAddress = new CustomerAddress
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Firstname = importOrder.ShippingAddress.Firstname,
                    Lastname = importOrder.ShippingAddress.Lastname,
                    CompanyName = importOrder.ShippingAddress.CompanyName,
                    Street = importOrder.ShippingAddress.Street,
                    City = importOrder.ShippingAddress.City,
                    Zip = importOrder.ShippingAddress.Zip,
                    Country = shippingAddressCountry,
                    CountryId = shippingAddressCountry.Id,
                };
                
                await _customerRepository.AddCustomerAddressAsync(newAddress);
            }

            var newOrder = new Order
            {
                SalesChannelId = salesChannel.Id,
                RemoteOrderId = importOrder.RemoteOrderId,
                CustomerId = customer.Id,
                Status = importOrder.Status,

                PaymentStatus = importOrder.PaymentStatus,
                PaymentMethod = importOrder.PaymentMethod,
                PaymentProvider = importOrder.PaymentProvider,
                PaymentTransactionId = importOrder.PaymentTransactionId,

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
                InvoiceAddressCountry = billingAddressCountry.Name,

                DeliveryAddressFirstName = importOrder.ShippingAddress.Firstname,
                DeliveryAddressLastName = importOrder.ShippingAddress.Lastname,
                DeliveryAddressCompanyName = importOrder.ShippingAddress.CompanyName,
                DeliveryAddressStreet = importOrder.ShippingAddress.Street,
                DeliveryAddressCity = importOrder.ShippingAddress.City,
                DeliverAddressZip = importOrder.ShippingAddress.Zip,
                DeliveryAddressCountry = shippingAddressCountry.Name,

                DateOrdered = importOrder.DateOrdered.ToUniversalTime()
            };

            if (importOrder.Items != null && importOrder.Items.Count > 0)
            {
                foreach (var item in importOrder.Items)
                {
                    if(String.IsNullOrEmpty(item.Sku))
                    {
                        _logger.LogError("Order {0}: Cannot import, product has empty SKU", importOrder.RemoteOrderId);
                        return;
                    }

                    var product = await _productRepository.GetBySkuAsync(item.Sku);

                    var newOrderItem = new OrderItem
                    {
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        TaxRate = item.TaxRate
                    };

                    if(product != null)
                    {
                        newOrderItem.ProductId = product.Id;
                        _logger.LogInformation("Order {0}: Add Item {1}", importOrder.RemoteOrderId, item.Name);
                    }
                    else
                    {
                        newOrderItem.MissingProductSku = item.Sku;
                        newOrderItem.MissingProductEan = item.Ean;

                        _logger.LogInformation("Order {0}: Cannot import, product with SKU {1} not found", importOrder.RemoteOrderId, item.Sku);
                    }

                    newOrder.OrderItems.Add(newOrderItem);
                }
            }

            await _orderRepository.CreateAsync(newOrder);
            _logger.LogInformation("Order {0}: created", importOrder.RemoteOrderId);

            if(salesChannel.ImportProducts == false)
            {
               _logger.LogInformation("Order {0}: SalesChannel product import is disabled, updating Stock", importOrder.RemoteOrderId);

                foreach (var orderItem in newOrder.OrderItems)
                {
                    Product product = await _productRepository.GetWithDetailsAsync(orderItem.ProductId) ?? throw new Exception("OrderImportRepository: Product not found");
                    product.ProductStocks.FirstOrDefault(w => w.WarehouseId == salesChannel.WarehouseId)!.Stock -= orderItem.Quantity;
                    await _productRepository.UpdateAsync(product);
                    _logger.LogInformation("Order {0}: Stock updated for product {1}", importOrder.RemoteOrderId, product.Sku);
                }
            }
        }
        else
        {
            _logger.LogInformation("Order {0}: already exists, check for changes", existingOrder.RemoteOrderId);
            bool somethingChanged = false;

            if(existingOrder.Status != importOrder.Status)
            {
                existingOrder.Status = importOrder.Status;
                somethingChanged = true;
                _logger.LogInformation("Order {0}: Status updated, new status is {1}", importOrder.RemoteOrderId, importOrder.Status);
            }

            if (somethingChanged)
            {
                await _orderRepository.UpdateAsync(existingOrder);
                _logger.LogInformation("Order {0}: updated", importOrder.RemoteOrderId);
            }
            else
            {
                _logger.LogInformation("Order {0}: has no changes", importOrder.RemoteOrderId);
            }
        }
    }

    private async Task<Country?> MapCountryFromStringAsync(string countryString)
    {
        return await _countryRepository.GetCountryByString(countryString);
    }
}