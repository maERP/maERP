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

                await Task.Delay(new TimeSpan(0, 0, 60)); // 60 Sekunden Verzögerung

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

            // Prüfen, ob Produkt-Import abgeschlossen ist
            if (salesChannel.ImportProducts && !salesChannel.InitialProductImportCompleted)
            {
                _logger.LogInformation($"Initial Product Import not completed for {salesChannel.Name} (ID: {salesChannel.Id})");
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
                            // Grundlegende Bestellinformationen
                            var importOrder = new SalesChannelImportOrder
                            {
                                RemoteOrderId = remoteOrder.LineItemId,
                                RemoteCustomerId = ordersResponse.Buyer?.Username ?? "",
                                DateOrdered = ordersResponse.CreationDate,
                                Subtotal = ordersResponse.PricingSummary?.Subtotal?.Value ?? 0m,
                                Total = ordersResponse.PricingSummary?.Total?.Value ?? remoteOrder.LineItemCost.Value,
                                TotalTax = ordersResponse.PricingSummary?.TotalTaxAmount?.Value ?? 0m,
                                ShippingCost = CalculateShippingCost(ordersResponse),
                                Status = MapOrderStatus(ordersResponse.OrderFulfillmentStatus),
                                PaymentStatus = MapPaymentStatus(ordersResponse.OrderPaymentStatus),
                                PaymentMethod = "eBay",
                                PaymentProvider = "eBay",
                                CustomerNote = "",
                                OrderItems = new List<SalesChannelImportOrderItem>(),
                                Customer = new SalesChannelImportCustomer()
                            };

                            // Kundeninformationen extrahieren
                            var buyer = ordersResponse.Buyer;
                            if (buyer != null && buyer.TaxAddress != null)
                            {
                                importOrder.Customer = new SalesChannelImportCustomer
                                {
                                    Email = buyer.TaxAddress?.Email ?? "",
                                    RemoteCustomerId = buyer.Username ?? "",
                                    Firstname = buyer.TaxAddress?.FirstName ?? "",
                                    Lastname = buyer.TaxAddress?.LastName ?? "",
                                    Phone = buyer.TaxAddress?.Phone ?? "",
                                    CustomerStatus = CustomerStatus.Active,
                                    DateEnrollment = DateTime.UtcNow
                                };

                                // Rechnungsadresse
                                if (buyer.TaxAddress != null)
                                {
                                    importOrder.Customer.BillingAddress = new SalesChannelImportCustomerAddress
                                    {
                                        Firstname = buyer.TaxAddress.FirstName ?? "",
                                        Lastname = buyer.TaxAddress.LastName ?? "",
                                        Street = buyer.TaxAddress.AddressLine1 ?? "",
                                        City = buyer.TaxAddress.City ?? "",
                                        Zip = buyer.TaxAddress.PostalCode ?? "",
                                        Country = buyer.TaxAddress.CountryCode ?? ""
                                    };

                                    importOrder.BillingAddress = new SalesChannelImportCustomerAddress
                                    {
                                        Firstname = buyer.TaxAddress.FirstName ?? "",
                                        Lastname = buyer.TaxAddress.LastName ?? "",
                                        Street = buyer.TaxAddress.AddressLine1 ?? "",
                                        City = buyer.TaxAddress.City ?? "",
                                        Zip = buyer.TaxAddress.PostalCode ?? "",
                                        Country = buyer.TaxAddress.CountryCode ?? ""
                                    };
                                }

                                // Lieferadresse
                                var shipTo = GetShippingAddress(ordersResponse);
                                if (shipTo != null)
                                {
                                    importOrder.Customer.ShippingAddress = new SalesChannelImportCustomerAddress
                                    {
                                        Firstname = shipTo.FirstName ?? "",
                                        Lastname = shipTo.LastName ?? "",
                                        Street = shipTo.AddressLine1 ?? "",
                                        City = shipTo.City ?? "",
                                        Zip = shipTo.PostalCode ?? "",
                                        Country = shipTo.CountryCode ?? ""
                                    };

                                    importOrder.ShippingAddress = new SalesChannelImportCustomerAddress
                                    {
                                        Firstname = shipTo.FirstName ?? "",
                                        Lastname = shipTo.LastName ?? "",
                                        Street = shipTo.AddressLine1 ?? "",
                                        City = shipTo.City ?? "",
                                        Zip = shipTo.PostalCode ?? "",
                                        Country = shipTo.CountryCode ?? ""
                                    };
                                }
                                else if (importOrder.Customer.BillingAddress != null)
                                {
                                    // Wenn keine Lieferadresse gefunden wurde, verwenden wir die Rechnungsadresse
                                    importOrder.Customer.ShippingAddress = importOrder.Customer.BillingAddress;
                                    importOrder.ShippingAddress = importOrder.BillingAddress;
                                }
                            }

                            // Bestellpositionen hinzufügen
                            importOrder.OrderItems.Add(new SalesChannelImportOrderItem
                            {
                                Name = remoteOrder.Title ?? "",
                                Sku = remoteOrder.Sku ?? "",
                                Quantity = remoteOrder.Quantity,
                                Price = remoteOrder.LineItemCost.Value / Math.Max(1, remoteOrder.Quantity),
                                TaxRate = 19 // Standard-Steuersatz, sollte in einer echten Anwendung dynamisch ermittelt werden
                            });

                            // Bestellung importieren oder aktualisieren
                            await orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importOrder);
                            _logger.LogInformation($"Order {importOrder.RemoteOrderId} imported or updated");
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

    private EbayAddress GetShippingAddress(EbayOrderResponse ordersResponse)
    {
        if (ordersResponse.FulfillmentStartInstructions != null &&
            ordersResponse.FulfillmentStartInstructions.Length > 0 &&
            ordersResponse.FulfillmentStartInstructions[0].ShippingStep != null)
        {
            return ordersResponse.FulfillmentStartInstructions[0].ShippingStep.ShipTo;
        }
        return null;
    }

    private decimal CalculateShippingCost(EbayOrderResponse ordersResponse)
    {
        // In einer echten Anwendung würde man hier die Versandkosten aus den eBay-Daten extrahieren
        // Da die Versandkosten in der aktuellen API-Struktur nicht direkt verfügbar sind,
        // berechnen wir sie als Differenz zwischen Gesamtpreis und Zwischensumme
        if (ordersResponse.PricingSummary != null &&
            ordersResponse.PricingSummary.Total != null &&
            ordersResponse.PricingSummary.Subtotal != null)
        {
            decimal total = ordersResponse.PricingSummary.Total.Value;
            decimal subtotal = ordersResponse.PricingSummary.Subtotal.Value;
            decimal taxAmount = ordersResponse.PricingSummary.TotalTaxAmount?.Value ?? 0m;

            return Math.Max(0, total - subtotal - taxAmount);
        }
        return 0m;
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