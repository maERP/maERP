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

            var newOrder = new Order
            {
                SalesChannelId = salesChannelId,
                RemoteOrderId = importOrder.RemoteOrderId,
                CustomerId = customer.Id,
                Status = importOrder.Status,
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