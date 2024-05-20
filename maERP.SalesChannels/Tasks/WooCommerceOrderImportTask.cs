#nullable disable

using System.ComponentModel.DataAnnotations;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace maERP.SalesChannels.Tasks;

public class WooCommerceOrderImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<WooCommerceOrderImportTask> _logger;

    public WooCommerceOrderImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<WooCommerceOrderImportTask> logger)
    {
        _service = serviceScopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("WooCommerceOrderImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 60 second delay

                _logger.LogInformation("WooCommerceOrderImportTask MainLoop finished");
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task MainLoop()
    {
        var scope = _service.CreateScope();

        var salesChannelRepository = scope.ServiceProvider.GetService<ISalesChannelRepository>();
        var orderImportRepository = scope.ServiceProvider.GetService<IOrderImportRepository>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.WooCommerce || salesChannel.ImportOrders != true)
            {
                continue;
            }

            _logger.LogInformation($"Start OrderDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            salesChannel.URL += "/wp-json/wc/v3/";

            RestAPI rest = new RestAPI(salesChannel.URL, salesChannel.Username, salesChannel.Password);
            WCObject wc = new WCObject(rest);

            var remoteOrders = await wc.Order.GetAll();

            if (remoteOrders.Count > 0)
            { 
                foreach (var remoteOrder in remoteOrders)
                {
                    // WooCommerce does not provide a subtotal
                    decimal subtotal = remoteOrder.total ?? 0;
                    subtotal -= remoteOrder.total_tax ?? 0;
                    subtotal -= remoteOrder.shipping_total ?? 0;
                    
                    var salesChannelImportOrder = new SalesChannelImportOrder
                    {
                        RemoteOrderId = remoteOrder.id.ToString(),
                        DateOrdered = remoteOrder.date_created ?? throw new ValidationException(),
                        Status = MapOrderStatus(remoteOrder.status),
                        
                        ShippingMethod = remoteOrder.shipping_lines.FirstOrDefault()?.method_title ?? string.Empty,
                        ShippingStatus = null,
                        ShippingProvider = null,
                        ShippingTrackingId = null,

                        Subtotal = subtotal,
                        ShippingCost = remoteOrder.shipping_total ?? 0,
                        TotalTax = remoteOrder.total_tax ?? 0,
                        Total = remoteOrder.total ?? 0,

                        Customer = new SalesChannelImportCustomer
                        {
                            Firstname = remoteOrder.billing.first_name,
                            Lastname = remoteOrder.billing.last_name,
                            CompanyName = remoteOrder.billing.company,
                            Email = remoteOrder.billing.email,
                            Phone = remoteOrder.billing.phone,
                        },
                        
                        BillingAddress = new SalesChannelImportCustomerAddress
                        {
                            Firstname = remoteOrder.billing.first_name,
                            Lastname = remoteOrder.billing.last_name,
                            CompanyName = remoteOrder.billing.company,
                            Street = remoteOrder.billing.address_1,
                            City = remoteOrder.billing.city,
                            Zip = remoteOrder.billing.postcode,
                            Country = remoteOrder.billing.country
                        },
                        
                        ShippingAddress = new SalesChannelImportCustomerAddress 
                        {
                            Firstname = remoteOrder.shipping.first_name,
                            Lastname = remoteOrder.shipping.last_name,
                            CompanyName = remoteOrder.shipping.company,
                            Street = remoteOrder.shipping.address_1,
                            City = remoteOrder.shipping.city,
                            Zip = remoteOrder.shipping.postcode,
                            Country = remoteOrder.shipping.country 
                        }
                    };
                    
                    await orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, salesChannelImportOrder);
                }
            }
        }
    }

    private OrderStatus MapOrderStatus(string orderStatus)
    {
        return orderStatus switch
        {
            "pending" => OrderStatus.Pending,
            "processing" => OrderStatus.Processing,
            "on-hold" => OrderStatus.OnHold,
            "completed" => OrderStatus.Completed,
            "cancelled" => OrderStatus.Cancelled,
            "refunded" => OrderStatus.Refunded,
            "failed" => OrderStatus.Failed,
            _ => OrderStatus.Unknown
        };
    }
}