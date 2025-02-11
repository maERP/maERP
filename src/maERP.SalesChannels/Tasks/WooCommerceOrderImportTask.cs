#nullable disable

using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
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

            salesChannel.Url += "/wp-json/wc/v3/";

            RestAPI rest = new RestAPI(salesChannel.Url, salesChannel.Username, salesChannel.Password);
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
                        DateOrdered = remoteOrder.date_created ?? DateTime.UtcNow,
                        Status = MapOrderStatus(remoteOrder.status),
                        PaymentStatus = PaymentStatus.Unknown,

                        PaymentMethod = remoteOrder.payment_method,
                        PaymentProvider = remoteOrder.payment_method_title,
                        PaymentTransactionId = remoteOrder.transaction_id,

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
                            DateEnrollment = remoteOrder.date_created_gmt ?? DateTime.UtcNow
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

                    salesChannelImportOrder.Items = remoteOrder.line_items.Select(item => new SalesChannelImportOrderItem
                    {
                        Name = item.name,
                        Sku = item.sku,
                        Quantity = (double)item.quantity!,
                        Price = (decimal)item.price!,
                        TaxRate = String.IsNullOrEmpty(item.tax_class) ? 0 : Convert.ToDouble(item.tax_class),
                    }).ToList();

                    await orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, salesChannelImportOrder);
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

    // ReSharper disable once UnusedMember.Local
    private PaymentStatus MapPaymentStatus(int orderPaymentStatusId)
    {
        return orderPaymentStatusId switch
        {
            1 => PaymentStatus.Invoiced,
            2 => PaymentStatus.PartiallyPaid,
            3 => PaymentStatus.CompletelyPaid,
            4 => PaymentStatus.FirstReminder,
            5 => PaymentStatus.SecondReminder,
            6 => PaymentStatus.ThirdReminder,
            7 => PaymentStatus.Encashment,
            8 => PaymentStatus.Reserved,
            9 => PaymentStatus.Delayed,
            10 => PaymentStatus.ReCrediting,
            11 => PaymentStatus.ReviewNecessary,
            12 => PaymentStatus.NoCreditApproved,
            13 => PaymentStatus.CreditPreliminarilyAccepted,
            _ => PaymentStatus.Unknown
        };
    }
}