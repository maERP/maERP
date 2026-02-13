#nullable disable

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.Shopware5;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Tasks;

public class Shopware5ProductImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<Shopware5ProductImportTask> _logger;

    public Shopware5ProductImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<Shopware5ProductImportTask> logger)
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
                _logger.LogDebug("Shopware5ProductImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 5 second delay

                _logger.LogDebug("Shopware5ProductImportTask MainLoop finished");
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

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.Shopware5 || salesChannel.ImportProducts == false)
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

            int requestStart = 0;
            int requestLimit = 100;
            int requestMax = 0;

            do
            {
                try
                {
                    var client = new HttpClient();
                    string requestUrl = salesChannel.Url + $"/api/articles?start={requestStart}&limit={requestLimit}";
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var authenticationString = $"{salesChannel.Username}:{salesChannel.Password}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

                    var response = await client.GetAsync(requestUrl).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        BaseListResponse<ProductResponse> remoteProducts = new();

                        try
                        {
                            remoteProducts = JsonSerializer.Deserialize<BaseListResponse<ProductResponse>>(result);

                            requestMax = remoteProducts.total;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Import Product error: {ex.Message}");
                        }

                        foreach (var remoteProduct in remoteProducts.data)
                        {
                            var importProduct = new SalesChannelImportProduct();

                            if (remoteProduct.mainDetail.ean.Length > 13)
                            {
                                remoteProduct.mainDetail.ean = remoteProduct.mainDetail.ean.Substring(0, 13);
                            }

                            if (remoteProduct.descriptionLong.Length > 4000)
                            {
                                remoteProduct.descriptionLong = remoteProduct.descriptionLong.Substring(0, 4000);
                            }

                            importProduct.Name = remoteProduct.name;
                            importProduct.Ean = remoteProduct.mainDetail.ean;
                            importProduct.Price = (decimal)remoteProduct.mainDetail.purchasePrice;
                            importProduct.Sku = remoteProduct.mainDetail.number;
                            importProduct.TaxRate = 19;
                            importProduct.Description = remoteProduct.descriptionLong;

                            await productImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, importProduct);
                        }

                        response.Dispose();

                        requestStart += requestLimit;

                        _logger.LogDebug($"Import Products: {requestUrl} (max {requestMax} Products)");

                        await Task.Delay(new TimeSpan(0, 0, 1)); // 5 second delay
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            while (requestMax != 0 && requestStart <= requestMax);

            if (salesChannel.InitialProductImportCompleted == false)
            {
                salesChannel.InitialProductImportCompleted = true;
                await salesChannelRepository.UpdateAsync(salesChannel);
            }
        }
    }
}