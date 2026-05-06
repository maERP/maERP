using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Repositories;

public class SalesImportRepository : ISalesImportRepository
{
    private readonly ILogger<ProductImportRepository> _logger;
    private readonly ISalesRepository _salesRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IProductRepository _productRepository;

    public SalesImportRepository(
        ILogger<ProductImportRepository> logger,
        ISalesRepository salesRepository,
        ICustomerRepository customerRepository,
        ICountryRepository countryRepository,
        IProductRepository productRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
        _customerRepository = customerRepository;
        _countryRepository = countryRepository;
        _productRepository = productRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(SalesChannel salesChannel, SalesChannelImportSales importSales)
    {
        var existingSales = await _salesRepository.GetByRemoteSalesIdAsync(salesChannel.Id, importSales.RemoteSalesId);

        if (existingSales == null)
        {
            _logger.LogInformation("Sales {0}: does not exist, create sales...", importSales.RemoteSalesId);

            // try to find customer in sales channel
            var customer = await _customerRepository.GetCustomerByRemoteCustomerIdAsync(salesChannel.Id, importSales.RemoteCustomerId);

            // when not found, try to find via email
            if (customer == null && importSales.Customer != null && !string.IsNullOrEmpty(importSales.Customer.Email))
            {
                customer = await _customerRepository.GetCustomerByEmailAsync(importSales.Customer.Email);

                // when found, add to CustomerSalesChannel
                if (customer != null)
                {
                    await _customerRepository.AddCustomerToSalesChannelAsync(customer.Id, salesChannel.Id, importSales.RemoteCustomerId);
                    _logger.LogInformation("CustomerSalesChannel added for Customer {0} ", customer.Id);
                }
            }

            // when still not found, create new customer
            if (customer == null)
            {
                var newCustomer = new Customer
                {
                    Email = importSales.Customer?.Email ?? string.Empty,
                    Firstname = importSales.Customer?.Firstname ?? string.Empty,
                    Lastname = importSales.Customer?.Lastname ?? string.Empty,
                    CompanyName = importSales.Customer?.CompanyName ?? string.Empty,
                    Phone = importSales.Customer?.Phone ?? string.Empty,
                    Website = importSales.Customer?.Website ?? string.Empty,
                    VatNumber = importSales.Customer?.VatNumber ?? string.Empty,
                    Note = importSales.Customer?.Note ?? string.Empty,
                    CustomerStatus = importSales.Customer?.CustomerStatus ?? CustomerStatus.Active,
                    DateEnrollment = importSales.Customer?.DateEnrollment ?? DateTime.UtcNow,
                };

                await _customerRepository.CreateAsync(newCustomer);
                _logger.LogInformation("Customer {0} created", importSales.Customer?.Email);
                customer = newCustomer;

                await _customerRepository.AddCustomerToSalesChannelAsync(newCustomer.Id, salesChannel.Id, importSales.RemoteCustomerId);
                _logger.LogInformation("CustomerSalesChannel added for Customer {0} ", customer.Id);
            }

            Guid billingAddressId = Guid.Empty;
            Guid shippingAddressId = Guid.Empty;
            var customerAddresses = await _customerRepository.GetCustomerAddressByCustomerIdAsync(customer.Id);

            Country? billingAddressCountry = await MapCountryFromStringAsync(importSales.BillingAddress.Country);
            Country? shippingAddressCountry = await MapCountryFromStringAsync(importSales.ShippingAddress.Country);

            if (billingAddressCountry == null)
            {
                _logger.LogError("Sales {0}: Cannot import, country {1} not found", importSales.RemoteSalesId, importSales.BillingAddress.Country);
                return;
            }

            if (shippingAddressCountry == null)
            {
                _logger.LogError("Sales {0}: Cannot import, country {1} not found", importSales.RemoteSalesId, importSales.ShippingAddress.Country);
                return;
            }

            foreach (var address in customerAddresses)
            {
                if (address.Firstname == importSales.BillingAddress.Firstname &&
                    address.Lastname == importSales.BillingAddress.Lastname &&
                    address.CompanyName == importSales.BillingAddress.CompanyName &&
                    address.Street == importSales.BillingAddress.Street &&
                    address.City == importSales.BillingAddress.City &&
                    address.Zip == importSales.BillingAddress.Zip)
                {
                    billingAddressId = address.Id;
                }

                if (address.Firstname == importSales.ShippingAddress.Firstname &&
                    address.Lastname == importSales.ShippingAddress.Lastname &&
                    address.CompanyName == importSales.ShippingAddress.CompanyName &&
                    address.Street == importSales.ShippingAddress.Street &&
                    address.City == importSales.ShippingAddress.City &&
                    address.Zip == importSales.ShippingAddress.Zip)
                {
                    shippingAddressId = address.Id;
                }

                if (billingAddressId != Guid.Empty && shippingAddressId != Guid.Empty)
                {
                    break;
                }
            }

            if (billingAddressId == Guid.Empty)
            {
                var newAddress = new CustomerAddress
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Firstname = importSales.BillingAddress.Firstname,
                    Lastname = importSales.BillingAddress.Lastname,
                    CompanyName = importSales.BillingAddress.CompanyName,
                    Street = importSales.BillingAddress.Street,
                    City = importSales.BillingAddress.City,
                    Zip = importSales.BillingAddress.Zip,
                    Country = billingAddressCountry,
                    CountryId = billingAddressCountry.Id
                };

                await _customerRepository.AddCustomerAddressAsync(newAddress);
            }

            if (shippingAddressId != Guid.Empty && shippingAddressId != billingAddressId)
            {
                var newAddress = new CustomerAddress
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Firstname = importSales.ShippingAddress.Firstname,
                    Lastname = importSales.ShippingAddress.Lastname,
                    CompanyName = importSales.ShippingAddress.CompanyName,
                    Street = importSales.ShippingAddress.Street,
                    City = importSales.ShippingAddress.City,
                    Zip = importSales.ShippingAddress.Zip,
                    Country = shippingAddressCountry,
                    CountryId = shippingAddressCountry.Id,
                };

                await _customerRepository.AddCustomerAddressAsync(newAddress);
            }

            var newSales = new Sales
            {
                SalesChannelId = salesChannel.Id,
                RemoteSalesId = importSales.RemoteSalesId,
                CustomerId = customer.CustomerId,
                Status = importSales.Status,

                PaymentStatus = importSales.PaymentStatus,
                PaymentMethod = importSales.PaymentMethod,
                PaymentProvider = importSales.PaymentProvider,
                PaymentTransactionId = importSales.PaymentTransactionId,

                Subtotal = importSales.Subtotal,
                ShippingCost = importSales.ShippingCost,
                TotalTax = importSales.TotalTax,
                Total = importSales.Total,

                InvoiceAddressFirstName = importSales.BillingAddress.Firstname,
                InvoiceAddressLastName = importSales.BillingAddress.Lastname,
                InvoiceAddressCompanyName = importSales.BillingAddress.CompanyName,
                InvoiceAddressStreet = importSales.BillingAddress.Street,
                InvoiceAddressCity = importSales.BillingAddress.City,
                InvoiceAddressZip = importSales.BillingAddress.Zip,
                InvoiceAddressCountry = billingAddressCountry.Name,

                DeliveryAddressFirstName = importSales.ShippingAddress.Firstname,
                DeliveryAddressLastName = importSales.ShippingAddress.Lastname,
                DeliveryAddressCompanyName = importSales.ShippingAddress.CompanyName,
                DeliveryAddressStreet = importSales.ShippingAddress.Street,
                DeliveryAddressCity = importSales.ShippingAddress.City,
                DeliveryAddressZip = importSales.ShippingAddress.Zip,
                DeliveryAddressCountry = shippingAddressCountry.Name,

                DateSalesed = importSales.DateSalesed.ToUniversalTime()
            };

            if (importSales.SalesItems != null && importSales.SalesItems.Count > 0)
            {
                foreach (var item in importSales.SalesItems)
                {
                    if (String.IsNullOrEmpty(item.Sku))
                    {
                        _logger.LogError("Sales {0}: Cannot import, product has empty SKU", importSales.RemoteSalesId);
                        return;
                    }

                    var product = await _productRepository.GetBySkuAsync(item.Sku);

                    var newSalesItem = new SalesItem
                    {
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        TaxRate = item.TaxRate
                    };

                    if (product != null)
                    {
                        newSalesItem.ProductId = product.Id;
                        _logger.LogInformation("Sales {0}: Add Item {1}", importSales.RemoteSalesId, item.Name);
                    }
                    else
                    {
                        newSalesItem.MissingProductSku = item.Sku;
                        newSalesItem.MissingProductEan = item.Ean;

                        _logger.LogInformation("Sales {0}: Cannot import, product with SKU {1} not found", importSales.RemoteSalesId, item.Sku);
                    }

                    newSales.SalesItems.Add(newSalesItem);
                }
            }

            newSales.SalesHistories = new List<SalesHistory>
            {
                new SalesHistory
                {
                    UserId = Guid.Empty,
                    SalesId = newSales.Id,
                    SalesStatusNew = newSales.Status,
                    PaymentStatusNew = newSales.PaymentStatus,
                    // TODO: implement ShippingStatus on import
                    // ShippingStatusNew = newSales.ShippingStatus,
                    Description = $"Imported from {salesChannel.Name}",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            };

            await _salesRepository.CreateAsync(newSales);
            _logger.LogInformation("Sales {0}: created", importSales.RemoteSalesId);

            if (salesChannel.ImportProducts == false)
            {
                _logger.LogInformation("Sales {0}: SalesChannel product import is disabled, updating Stock", importSales.RemoteSalesId);

                foreach (var salesItem in newSales.SalesItems)
                {
                    Product product = await _productRepository.GetWithDetailsAsync(salesItem.ProductId) ?? throw new Exception("SalesImportRepository: Product not found");

                    // Use first warehouse of sales channel for stock update
                    var warehouse = salesChannel.Warehouses?.FirstOrDefault();
                    if (warehouse != null)
                    {
                        var productStock = product.ProductStocks.FirstOrDefault(w => w.WarehouseId == warehouse.Id);
                        if (productStock != null)
                        {
                            productStock.Stock -= salesItem.Quantity;
                            await _productRepository.UpdateAsync(product);
                            _logger.LogInformation("Sales {0}: Stock updated for product {1} in warehouse {2}", importSales.RemoteSalesId, product.Sku, warehouse.Name);
                        }
                        else
                        {
                            _logger.LogWarning("Sales {0}: No stock found for product {1} in warehouse {2}", importSales.RemoteSalesId, product.Sku, warehouse.Name);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Sales {0}: SalesChannel has no warehouses configured, cannot update stock for product {1}", importSales.RemoteSalesId, product.Sku);
                    }
                }
            }
        }
        else
        {
            _logger.LogInformation("Sales {0}: already exists, check for changes", existingSales.RemoteSalesId);
            bool somethingChanged = false;

            if (existingSales.Status != importSales.Status)
            {
                somethingChanged = true;
                _logger.LogInformation("Sales {0}: Status updated, new status is {1}", importSales.RemoteSalesId, importSales.Status);
            }

            if (existingSales.PaymentStatus != importSales.PaymentStatus)
            {
                somethingChanged = true;
                _logger.LogInformation("Sales {0}: PaymentStatus updated, new status is {1}", importSales.RemoteSalesId, importSales.PaymentStatus);
            }

            // TODO: implement check for changed shipping status

            if (somethingChanged)
            {
                await _salesRepository.UpdateAsync(existingSales);
                _logger.LogInformation("Sales {0}: updated", importSales.RemoteSalesId);
            }
            else
            {
                _logger.LogInformation("Sales {0}: has no changes", importSales.RemoteSalesId);
            }
        }
    }

    private async Task<Country?> MapCountryFromStringAsync(string countryString)
    {
        return await _countryRepository.GetCountryByString(countryString);
    }
}