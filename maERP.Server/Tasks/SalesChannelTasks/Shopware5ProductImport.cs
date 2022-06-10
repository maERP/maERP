#nullable disable

using System.Net.Http.Headers;
using maERP.Data.Models;
using maERP.Server.Contracts;
using Newtonsoft.Json;
using maERP.Data.Models.SalesChannels.Shopware5;
using maERP.Data.Models.SalesChannels.Shopware5.ProductResponse;

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
        Console.WriteLine("ProductDownloadTask started!");

        using (var scope = _service.CreateScope())
        {
            var salesChannelRepository = scope.ServiceProvider.GetService<ISalesChannelRepository>();
            var productRepository = scope.ServiceProvider.GetService<IProductsRepository>();
            var productSalesChannelRepository = scope.ServiceProvider.GetService<IProductsSalesChannelsRepository>();

            var salesChannels = await salesChannelRepository.GetAllAsync();

            foreach (var salesChannel in salesChannels)
            {
                if (salesChannel.Type == SalesChannelType.shopware5 && salesChannel.ImportProducts == true)
                {
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

                                Shopware5Response<Shopware5ProductResponse> remoteProducts =  new Shopware5Response<Shopware5ProductResponse>();

                                try
                                {
                                    remoteProducts = JsonConvert.DeserializeObject<Shopware5Response<Shopware5ProductResponse>>(result);

                                    requestMax = remoteProducts.total;
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine($"Import Product error: {ex.Message}");
                                }

                                foreach (var remoteProduct in remoteProducts.data)
                                {
                                    var localProduct = new Product();

                                    try
                                    {
                                        bool exists = await productRepository.Exists(remoteProduct.id);

                                        if (exists)
                                        {
                                            localProduct = await productRepository.GetAsync(remoteProduct.id);
                                        }

                                        localProduct.Id = remoteProduct.id;
                                        localProduct.Name = remoteProduct.name;
                                        localProduct.EAN = Globals.maxLength(remoteProduct.mainDetail.ean, 13);
                                        localProduct.Price = (decimal)remoteProduct.mainDetail.purchasePrice;
                                        localProduct.SKU = remoteProduct.mainDetail.number;
                                        localProduct.TaxClassId = 1;
                                        localProduct.Description = Globals.maxLength(remoteProduct.descriptionLong, 4000);

                                        if(exists)
                                        {
                                            await productRepository.UpdateAsync(localProduct);
                                        }
                                        else
                                        {
                                            await productRepository.AddAsync(localProduct);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                                response.Dispose();

                                requestStart += requestLimit;

                                Console.WriteLine($"Import Products: {requestUrl} (max {requestMax} Products)");

                                await Task.Delay(new TimeSpan(0, 0, 1)); // 5 second delay
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        }
                    }
                    while (requestMax != 0 && requestStart <= requestMax);

                    Console.WriteLine("Download function end");
                }
            }
        }
    }
}