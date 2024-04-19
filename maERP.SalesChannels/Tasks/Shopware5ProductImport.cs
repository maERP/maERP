#nullable disable

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.SalesChannel;
using maERP.SalesChannels.Repositories;
using maERP.Domain.Models;
using maERP.Domain.Models.SalesChannelData;
using maERP.Domain.Models.SalesChannelData.Shopware5;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.SalesChannels.Tasks;

public class ProductDownloadTask : IHostedService
{
    private readonly IServiceScopeFactory _service;

    public ProductDownloadTask(IServiceScopeFactory serviceScopeFactory)
    {
        _service = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 5 second delay

                Console.WriteLine("ProductDownload MainLoop finished");
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
        var productRepository = scope.ServiceProvider.GetService<IProductRepository>();
        var productSalesChannelRepository = scope.ServiceProvider.GetService<IProductSalesChannelRepository>();
        var productImportRepository = scope.ServiceProvider.GetService<IProductImportRepository>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.shopware5 || salesChannel.ImportProducts == false)
            {
                continue;
            }

            Console.WriteLine($"Start ProductDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            int requestStart = 0;
            int requestLimit = 100;
            int requestMax = 0;

            do
            {
                try
                {
                    var client = new HttpClient();
                    string requestUrl = salesChannel.URL + $"/api/articles?start={requestStart}&limit={requestLimit}";
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var authenticationString = $"{salesChannel.Username}:{salesChannel.Password}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(requestUrl).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        Shopware5Response<Shopware5ProductResponse> remoteProducts = new();

                        try
                        {
                            remoteProducts = JsonSerializer.Deserialize<Shopware5Response<Shopware5ProductResponse>>(result);

                            requestMax = remoteProducts.total;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Import Product error: {ex.Message}");

                            Console.WriteLine(result);
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

                        Console.WriteLine($"Import Products: {requestUrl} (max {requestMax} Products)");

                        await Task.Delay(new TimeSpan(0, 0, 1)); // 5 second delay
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            while (requestMax != 0 && requestStart <= requestMax);
        }
    }
}