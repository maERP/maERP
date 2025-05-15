#nullable disable

using System.Net.Http.Headers;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.eBay;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Tasks;

public class EbayOrderImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<EbayOrderImportTask> _logger;

    public EbayOrderImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<EbayOrderImportTask> logger)
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
                _logger.LogInformation("EbayOrderImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 60 second delay

                _logger.LogInformation("EbayOrderImportTask MainLoop finished");
            }
        });

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
        var authHelper = scope.ServiceProvider.GetService<EbayAuthHelper>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.eBay || salesChannel.ImportOrders == false)
            {
                continue;
            }

            _logger.LogInformation($"Start OrderDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            int offset = 0;
            int limit = 50;
            bool hasMoreOrders = true;

            try
            {
                // OAuth-Token holen
                string accessToken = await authHelper.GetAccessTokenAsync(salesChannel);
                
                while (hasMoreOrders)
                {
                    var client = new HttpClient();
                    string requestUrl = $"{salesChannel.Url}/sell/fulfillment/v1/order?limit={limit}&offset={offset}";
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var ordersResponse = JsonSerializer.Deserialize<EbayOrderResponse>(result);

                        if (ordersResponse.Total == 0 || ordersResponse.LineItems == null || ordersResponse.LineItems.Length == 0)
                        {
                            hasMoreOrders = false;
                            break;
                        }

                        foreach (var remoteOrder in ordersResponse.LineItems)
                        {
                            var importOrder = new SalesChannelImportOrder
                            {
                                RemoteOrderId = remoteOrder.LineItemId,
                                DateOrdered = ordersResponse.CreationDate,
                                Total = remoteOrder.LineItemCost.Value,
                                TotalTax = 0, // Steuerinformationen aus einem anderen API-Endpunkt ermitteln
                                Status = MapOrderStatus(ordersResponse.OrderFulfillmentStatus),
                                PaymentStatus = MapPaymentStatus(ordersResponse.OrderPaymentStatus),
                                PaymentMethod = "eBay",
                                CustomerNote = "",
                                OrderItems = new List<SalesChannelImportOrderItem>(),
                                Customer = new SalesChannelImportCustomer()
                            };

                            // Kundeninformationen extrahieren
                            var buyer = ordersResponse.Buyer;
                            if (buyer != null)
                            {
                                importOrder.Customer = new SalesChannelImportCustomer
                                {
                                    Email = buyer.TaxAddress?.Email ?? "",
                                    RemoteCustomerId = buyer.Username ?? "",
                                    Firstname = buyer.TaxAddress?.FirstName ?? "",
                                    Lastname = buyer.TaxAddress?.LastName ?? "",
                                    ShippingAddress = new SalesChannelImportCustomerAddress
                                    {
                                        Firstname = buyer.TaxAddress?.FirstName ?? "",
                                        Lastname = buyer.TaxAddress?.LastName ?? "",
                                        Street = buyer.TaxAddress?.AddressLine1 ?? "",
                                        City = buyer.TaxAddress?.City ?? "",
                                        Zip = buyer.TaxAddress?.PostalCode ?? "",
                                        Country = buyer.TaxAddress?.CountryCode ?? "",
                                        Phone = buyer.TaxAddress?.Phone ?? ""
                                    },
                                    BillingAddress = new SalesChannelImportCustomerAddress
                                    {
                                        Firstname = buyer.TaxAddress?.FirstName ?? "",
                                        Lastname = buyer.TaxAddress?.LastName ?? "",
                                        Street = buyer.TaxAddress?.AddressLine1 ?? "",
                                        City = buyer.TaxAddress?.City ?? "",
                                        Zip = buyer.TaxAddress?.PostalCode ?? "",
                                        Country = buyer.TaxAddress?.CountryCode ?? "",
                                        Phone = buyer.TaxAddress?.Phone ?? ""
                                    }
                                };
                            }

                            // Bestellpositionen hinzufügen
                            importOrder.OrderItems.Add(new SalesChannelImportOrderItem
                            {
                                Name = remoteOrder.Title,
                                Sku = remoteOrder.Sku,
                                Quantity = remoteOrder.Quantity,
                                Price = remoteOrder.LineItemCost.Value / remoteOrder.Quantity
                            });

                            await orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importOrder);
                        }

                        offset += limit;
                        
                        _logger.LogInformation($"Import Orders: {requestUrl} (offset {offset})");
                        
                        // API-Ratenbegrenzung beachten
                        await Task.Delay(new TimeSpan(0, 0, 1));
                    }
                    else
                    {
                        _logger.LogError($"Error fetching eBay orders: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                        hasMoreOrders = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in eBay order import: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    private OrderStatus MapOrderStatus(string ebayStatus)
    {
        return ebayStatus?.ToLower() switch
        {
            "not_started" => OrderStatus.Pending,
            "in_progress" => OrderStatus.Processing,
            "fulfilled" => OrderStatus.Completed,
            "failed" => OrderStatus.Failed,
            _ => OrderStatus.Unknown
        };
    }

    private PaymentStatus MapPaymentStatus(string ebayPaymentStatus)
    {
        return ebayPaymentStatus?.ToLower() switch
        {
            "paid" => PaymentStatus.CompletelyPaid,
            "partially_paid" => PaymentStatus.PartiallyPaid,
            "not_paid" => PaymentStatus.Invoiced,
            _ => PaymentStatus.Unknown
        };
    }
} 