#nullable disable

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.SalesChannels.Models.Shopware5;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using maERP.Domain.Models;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Repositories;
using maERP.SalesChannels.Contracts;

namespace maERP.SalesChannels.Tasks;

public class Shopware5OrderImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<Shopware5OrderImportTask> _logger;

    public Shopware5OrderImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<Shopware5OrderImportTask> logger)
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
                _logger.LogInformation("Shopware5OrderImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 60 second delay

                _logger.LogInformation("Shopware5OrderImportTask MainLoop finished");
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
        var orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();
        var orderImportRepository = scope.ServiceProvider.GetService<IOrderImportRepository>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.Shopware5 || salesChannel.ImportOrders != true)
            {
                continue;
            }

            _logger.LogInformation($"Start OrderDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            int requestStart = 0;
            int requestLimit = 100;
            int requestMax = 0;

            do
            {
                try
                {
                    var client = new HttpClient();
                    string requestUrl = salesChannel.URL + $"/api/orders?start={requestStart}&limit={requestLimit}";
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var authenticationString = $"{salesChannel.Username}:{salesChannel.Password}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

                    HttpResponseMessage response = new();
                    response = await client.GetAsync(requestUrl).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        Shopware5Response<Shopware5OrderResponse> remoteOrders = new();

                        try
                        {
                            remoteOrders = JsonSerializer.Deserialize<Shopware5Response<Shopware5OrderResponse>>(result);

                            if(remoteOrders.data == null || remoteOrders.success == false)
                            {
                                throw new Exception("No data in response");
                            }

                            requestMax = remoteOrders.total;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Import Order error: {ex.Message}");
                        }

                        foreach (var remoteOrder in remoteOrders.data)
                        {
                            _logger.LogInformation("Import Order {0}", remoteOrder.id.ToString());

                            var order = await orderRepository.GetByRemoteOrderIdAsync(salesChannel.Id, remoteOrder.id.ToString());

                            // new order
                            if (order == null)
                            {
                                var salesChannelImportOrder = new SalesChannelImportOrder
                                {
                                    RemoteOrderId = remoteOrder.id.ToString(),
                                    DateOrdered = DateTime.UtcNow, // remoteOrder.orderTime,
                                    Status = OrderStatus.Unknown, // MapOrderStatus(remoteOrder.status),

                                    ShippingMethod = string.Empty,
                                    ShippingStatus = string.Empty,
                                    ShippingProvider = string.Empty,
                                    ShippingTrackingId = string.Empty,

                                    Subtotal = remoteOrder.invoiceAmountNet,
                                    ShippingCost = remoteOrder.invoiceShippingNet,
                                    TotalTax = remoteOrder.invoiceAmount - remoteOrder.invoiceAmountNet,
                                    Total = remoteOrder.invoiceAmount,

                                    /*
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
                                    */
                                }; 

                                /*
                                salesChannelImportOrder.Items = remoteOrder.line_items.Select(item => new SalesChannelImportOrderItem
                                {
                                    Name = item.name,
                                    SKU = item.sku,
                                    Quantity = (double)item.quantity,
                                    Price = (decimal)item.price,
                                    TaxRate = item.tax_class.IsNullOrEmpty() ? 0 : Convert.ToDouble(item.tax_class),
                                }).ToList();
                                */

                                await orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, salesChannelImportOrder);
                            }
                            // existing order
                        }

                        response.Dispose();

                        requestStart += requestLimit;

                        _logger.LogInformation($"Import Orders: {requestUrl} (max {requestMax} Orders)");

                        await Task.Delay(new TimeSpan(0, 0, 1)); // 5 second delay
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            while (requestMax != 0 && requestStart <= requestMax);
        }
    }
}