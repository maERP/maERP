#nullable disable

using System.Net.Http.Headers;
using Newtonsoft.Json;
using maERP.Server.Contracts;
using maERP.Shared.Models;
using maERP.Shared.Models.SalesChannels.Shopware5;
using maERP.Shared.Models.SalesChannels.Shopware5.ProductResponse;

namespace maERP.Server.Tasks.SalesChannelTasks;

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

                Console.WriteLine("MainLoop finished");
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

                        Shopware5Response<Shopware5ProductResponse> remoteProducts = new Shopware5Response<Shopware5ProductResponse>();

                        try
                        {
                            remoteProducts = JsonConvert.DeserializeObject<Shopware5Response<Shopware5ProductResponse>>(result);

                            requestMax = remoteProducts.total;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Import Product error: {ex.Message}");
                        }

                        foreach (var remoteProduct in remoteProducts.data)
                        {
                            var ProductSalesChannel = await productSalesChannelRepository.getByRemoteProductIdAsync(remoteProduct.id, salesChannel.Id);

                            if (ProductSalesChannel == null)
                            {
                                var localProduct = new Product();

                                localProduct.Name = remoteProduct.name;
                                localProduct.Ean = Globals.maxLength(remoteProduct.mainDetail.ean, 13);
                                localProduct.Price = (decimal)remoteProduct.mainDetail.purchasePrice;
                                localProduct.Sku = remoteProduct.mainDetail.number;
                                // localProduct.TaxClassId = 1;
                                localProduct.Description = Globals.maxLength(remoteProduct.descriptionLong, 4000);

                                localProduct.ProductSalesChannel = new List<ProductSalesChannel>();

                                localProduct.ProductSalesChannel.Add(new ProductSalesChannel
                                {
                                    // RemoteProductId = remoteProduct.id,
                                    // SalesChannelId = salesChannel.Id,
                                    Price = (decimal)remoteProduct.mainDetail.purchasePrice
                                });

                                await productRepository.AddAsync(localProduct);
                            }
                            else
                            {
                                bool newUpdate = false;

                                ProductSalesChannel.Price = (decimal)remoteProduct.mainDetail.purchasePrice;

                                await productSalesChannelRepository.UpdateAsync(ProductSalesChannel);

                                // var localProduct = await productRepository.GetAsync(ProductSalesChannel.ProductId);
                                var localProduct = await productRepository.GetAsync(ProductSalesChannel.Product.Id);

                                if(localProduct.Name != remoteProduct.name)
                                {
                                    Console.WriteLine("new product name");
                                    localProduct.Name = remoteProduct.name;
                                    newUpdate = true;
                                }

                                if(localProduct.Ean != Globals.maxLength(remoteProduct.mainDetail.ean, 13))
                                {
                                    Console.WriteLine("new product EAN");
                                    localProduct.Ean = Globals.maxLength(remoteProduct.mainDetail.ean, 13);
                                    newUpdate = true;
                                }

                                if(localProduct.Price != (decimal)remoteProduct.mainDetail.purchasePrice)
                                {
                                    Console.WriteLine("new product price");
                                    localProduct.Price = (decimal)remoteProduct.mainDetail.purchasePrice;
                                    newUpdate = true;
                                }

                                if(localProduct.Ean != remoteProduct.mainDetail.number)
                                {
                                    Console.WriteLine("new product sku");
                                    localProduct.Ean = remoteProduct.mainDetail.number;
                                    newUpdate = true;
                                }

                                /* if(localProduct.TaxClassId != 1)
                                {
                                    Console.WriteLine("new product tax class");
                                    localProduct.TaxClassId = 1;
                                    newUpdate = true;
                                }*/

                                if(localProduct.Description != Globals.maxLength(remoteProduct.descriptionLong, 4000))
                                {
                                    Console.WriteLine("new product description");
                                    localProduct.Description = Globals.maxLength(remoteProduct.descriptionLong, 4000);
                                    newUpdate = true;
                                }

                                if(newUpdate)
                                {
                                    localProduct.UpdatedAt = DateTime.Now;
                                    await productRepository.UpdateAsync(localProduct);
                                }                                
                            }
                        }

                        response.Dispose();

                        requestStart += requestLimit;

                        Console.WriteLine($"Import Products: {requestUrl} (max {requestMax} Products)");

                        await Task.Delay(new TimeSpan(0, 0, 1)); // 5 second delay
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            while (requestMax != 0 && requestStart <= requestMax);
        }
    }
}