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

public class EbayCustomerImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<EbayCustomerImportTask> _logger;

    public EbayCustomerImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<EbayCustomerImportTask> logger)
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
                _logger.LogDebug("EbayCustomerImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 300)); // 5 Minuten Verzögerung

                _logger.LogDebug("EbayCustomerImportTask MainLoop finished");
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
        var customerImportRepository = scope.ServiceProvider.GetService<ICustomerImportRepository>();
        var authHelper = scope.ServiceProvider.GetService<EbayAuthHelper>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.eBay || salesChannel.ImportCustomers == false)
            {
                continue;
            }

            try
            {
                SalesChannelUrlValidator.Validate(salesChannel.Url);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Invalid sales channel URL for {Name} (ID: {Id}): {Message}", salesChannel.Name, salesChannel.Id, ex.Message);
                continue;
            }

            _logger.LogDebug($"Start CustomerDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            try
            {
                // OAuth-Token holen
                string accessToken = await authHelper.GetAccessTokenAsync(salesChannel);

                // eBay erlaubt nicht den direkten Abruf aller Kunden
                // Stattdessen müssen wir Kunden über ihre Bestellungen identifizieren
                await ImportCustomersFromOrders(salesChannel, customerImportRepository, accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in eBay customer import: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    private async Task ImportCustomersFromOrders(SalesChannel salesChannel, ICustomerImportRepository customerImportRepository, string accessToken)
    {
        int offset = 0;
        int limit = 50;
        bool hasMoreOrders = true;

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

                // Kunden aus den Bestellungen extrahieren und importieren
                foreach (var order in ordersResponse.LineItems)
                {
                    var buyer = ordersResponse.Buyer;
                    if (buyer != null && buyer.TaxAddress != null)
                    {
                        var importCustomer = new SalesChannelImportCustomer
                        {
                            RemoteCustomerId = buyer.Username ?? "",
                            Email = buyer.TaxAddress?.Email ?? "",
                            Firstname = buyer.TaxAddress?.FirstName ?? "",
                            Lastname = buyer.TaxAddress?.LastName ?? "",
                            Phone = buyer.TaxAddress?.Phone ?? "",
                            // Weitere Felder können aus der eBay API ergänzt werden
                            CustomerStatus = CustomerStatus.Active,
                            DateEnrollment = DateTime.UtcNow,

                            BillingAddress = new SalesChannelImportCustomerAddress
                            {
                                Firstname = buyer.TaxAddress?.FirstName ?? "",
                                Lastname = buyer.TaxAddress?.LastName ?? "",
                                Street = buyer.TaxAddress?.AddressLine1 ?? "",
                                City = buyer.TaxAddress?.City ?? "",
                                Zip = buyer.TaxAddress?.PostalCode ?? "",
                                Country = buyer.TaxAddress?.CountryCode ?? ""
                            }
                        };

                        // Bei eBay können die Shipping- und Billing-Adressen unterschiedlich sein
                        // Abhängig von der Bestellung
                        if (ordersResponse.FulfillmentStartInstructions != null &&
                            ordersResponse.FulfillmentStartInstructions.Length > 0 &&
                            ordersResponse.FulfillmentStartInstructions[0].ShippingStep != null &&
                            ordersResponse.FulfillmentStartInstructions[0].ShippingStep.ShipTo != null)
                        {
                            var shipTo = ordersResponse.FulfillmentStartInstructions[0].ShippingStep.ShipTo;

                            importCustomer.ShippingAddress = new SalesChannelImportCustomerAddress
                            {
                                Firstname = shipTo.FirstName ?? "",
                                Lastname = shipTo.LastName ?? "",
                                Street = shipTo.AddressLine1 ?? "",
                                City = shipTo.City ?? "",
                                Zip = shipTo.PostalCode ?? "",
                                Country = shipTo.CountryCode ?? ""
                            };
                        }
                        else
                        {
                            // Falls keine separate Lieferadresse vorhanden ist, Rechnungsadresse verwenden
                            importCustomer.ShippingAddress = importCustomer.BillingAddress;
                        }

                        await customerImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importCustomer);
                    }
                }

                offset += limit;

                _logger.LogDebug($"Processed {limit} orders for customer import (offset {offset})");

                // API-Ratenbegrenzung beachten
                await Task.Delay(new TimeSpan(0, 0, 1));
            }
            else
            {
                _logger.LogError($"Error fetching eBay orders for customer import: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                hasMoreOrders = false;
                break;
            }
        }
    }
}