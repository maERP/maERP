#nullable disable

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.eBay;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Tasks;

public class EbayProductImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<EbayProductImportTask> _logger;

    public EbayProductImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<EbayProductImportTask> logger)
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
                _logger.LogDebug("EbayProductImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 60 second delay

                _logger.LogDebug("EbayProductImportTask MainLoop finished");
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
        var productImportRepository = scope.ServiceProvider.GetService<IProductImportRepository>();
        var authHelper = scope.ServiceProvider.GetService<EbayAuthHelper>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.eBay || salesChannel.ImportProducts == false)
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

            _logger.LogDebug($"Start ProductDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            int offset = 0;
            int limit = 100;
            int total = 0;

            try
            {
                // OAuth-Token holen
                string accessToken = await authHelper.GetAccessTokenAsync(salesChannel);

                do
                {
                    // Erster Schritt: Inventardetails abrufen
                    var client = new HttpClient();
                    string requestUrl = $"{salesChannel.Url}/sell/inventory/v1/inventory_item?limit={limit}&offset={offset}";
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var inventoryItems = JsonSerializer.Deserialize<EbayInventoryItemResponse>(result);

                        total = inventoryItems.Total;

                        foreach (var item in inventoryItems.InventoryItems)
                        {
                            // Zweiter Schritt: Angebotsinformationen abrufen
                            string offerUrl = $"{salesChannel.Url}/sell/inventory/v1/offer?sku={item.Sku}";
                            var offerResponse = await client.GetAsync(offerUrl);

                            if (offerResponse.IsSuccessStatusCode)
                            {
                                string offerResult = await offerResponse.Content.ReadAsStringAsync();
                                var offers = JsonSerializer.Deserialize<EbayOfferResponse>(offerResult);

                                if (offers.Offers != null && offers.Offers.Length > 0)
                                {
                                    var offer = offers.Offers[0]; // Erstes Angebot nehmen
                                    var importProduct = new SalesChannelImportProduct
                                    {
                                        Name = item.Product.Title,
                                        Sku = item.Sku,
                                        Ean = item.Product.Ean != null && item.Product.Ean.Length > 0
                                            ? item.Product.Ean[0]
                                            : string.Empty,
                                        Price = offer.PricingSummary.Price.Value,
                                        TaxRate = 19, // Standard-Steuersatz
                                        Description = item.Product.Description ?? string.Empty
                                    };

                                    if (importProduct.Description.Length > 4000)
                                    {
                                        importProduct.Description = importProduct.Description.Substring(0, 4000);
                                    }

                                    await productImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, importProduct);
                                }
                            }
                        }

                        offset += limit;

                        _logger.LogDebug($"Import Products: {requestUrl} (max {total} Products)");

                        await Task.Delay(new TimeSpan(0, 0, 1));
                    }
                    else
                    {
                        _logger.LogError($"Error fetching eBay inventory: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                        break;
                    }
                }
                while (total > 0 && offset < total);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in eBay product import: {ex.Message}\n{ex.StackTrace}");
            }

            if (salesChannel.InitialProductImportCompleted == false)
            {
                salesChannel.InitialProductImportCompleted = true;
                await salesChannelRepository.UpdateAsync(salesChannel);
            }
        }
    }
}