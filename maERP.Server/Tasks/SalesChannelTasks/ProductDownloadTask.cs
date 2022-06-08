#nullable disable

using System.Net.Http.Headers;
using maERP.Data.Models;
using maERP.Server.Contracts;
using Newtonsoft.Json;

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
                MainLoop();

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

    private async void MainLoop()
    {
        Console.WriteLine("ProductDownloadTask started!");

        using (var scope = _service.CreateScope())
        {
            var salesChannelRepository = scope.ServiceProvider.GetService<ISalesChannelRepository>();
            var productRepository = scope.ServiceProvider.GetService<IProductsRepository>();

            var salesChannels = await salesChannelRepository.GetAllAsync();

            foreach (var salesChannel in salesChannels)
            {
                if (salesChannel.Type == SalesChannelType.shopware5 && salesChannel.ImportProducts == true)
                {
                    Console.WriteLine("Start ProductDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

                    var client = new HttpClient();
                    string requestUrl = salesChannel.URL + "/api/articles?start=0&limit=250";
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var authenticationString = $"{salesChannel.Username}:{salesChannel.Password}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(requestUrl).ConfigureAwait(true);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        var remoteProducts = JsonConvert.DeserializeObject<Shopware5RootResponse>(result);

                        foreach (var remoteProduct in remoteProducts.data)
                        {
                            try
                            {
                                bool exists = await productRepository.Exists(remoteProduct.id);

                                if (exists)
                                {
                                    var localProduct = await productRepository.GetAsync(remoteProduct.id);

                                    localProduct.Name = remoteProduct.name;
                                    localProduct.EAN = remoteProduct.mainDetail.ean.Substring(0, 13);
                                    localProduct.Price = (decimal)remoteProduct.mainDetail.purchasePrice;
                                    localProduct.SKU = remoteProduct.mainDetail.number;
                                    localProduct.Description = remoteProduct.descriptionLong.Substring(0, 4000);
                                    localProduct.TaxClassId = 1;

                                    await productRepository.UpdateAsync(localProduct);
                                }
                                else
                                {
                                    var localProduct = new Product();

                                    localProduct.Id = remoteProduct.id;
                                    localProduct.Name = remoteProduct.name;
                                    localProduct.EAN = remoteProduct.mainDetail.ean.Substring(0, 13);
                                    localProduct.Price = (decimal)remoteProduct.mainDetail.purchasePrice;
                                    localProduct.SKU = remoteProduct.mainDetail.number;
                                    localProduct.Description = remoteProduct.descriptionLong.Substring(0,4000);
                                    localProduct.TaxClassId = 1;

                                    await productRepository.AddAsync(localProduct);
                                }
                            }
                            catch(Exception)
                            {
                                Console.WriteLine("EAN: \"" + remoteProduct.mainDetail.ean + "\"");
                            }
                        }

                        response.Dispose();

                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}

public class Attribute
{
    public int id { get; set; }
    public int articleDetailId { get; set; }
    public object attr1 { get; set; }
    public object attr2 { get; set; }
    public object attr3 { get; set; }
    public object attr4 { get; set; }
    public object attr5 { get; set; }
    public object attr6 { get; set; }
    public object attr7 { get; set; }
    public object attr8 { get; set; }
    public object attr9 { get; set; }
    public object attr10 { get; set; }
    public object attr11 { get; set; }
    public object attr12 { get; set; }
    public object attr13 { get; set; }
    public object attr14 { get; set; }
    public object attr15 { get; set; }
    public object attr16 { get; set; }
    public object attr17 { get; set; }
    public object attr18 { get; set; }
    public object attr19 { get; set; }
    public object attr20 { get; set; }
    public object swagIsTrustedShopsArticle { get; set; }
    public object swagTrustedRange { get; set; }
    public object swagTrustedDuration { get; set; }
}

public class Shopware5Product
{
    public int id { get; set; }
    public int mainDetailId { get; set; }
    public int supplierId { get; set; }
    public int taxId { get; set; }
    public object priceGroupId { get; set; }
    public object filterGroupId { get; set; }
    public object configuratorSetId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string descriptionLong { get; set; }
    public DateTime added { get; set; }
    public bool active { get; set; }
    public int pseudoSales { get; set; }
    public bool highlight { get; set; }
    public string keywords { get; set; }
    public string metaTitle { get; set; }
    public DateTime changed { get; set; }
    public bool priceGroupActive { get; set; }
    public bool lastStock { get; set; }
    public int crossBundleLook { get; set; }
    public bool notification { get; set; }
    public string template { get; set; }
    public int mode { get; set; }
    public object availableFrom { get; set; }
    public object availableTo { get; set; }
    public MainDetail mainDetail { get; set; }
}

public class MainDetail
{
    public int id { get; set; }
    public int articleId { get; set; }
    public int unitId { get; set; }
    public string number { get; set; }
    public string supplierNumber { get; set; }
    public int kind { get; set; }
    public string additionalText { get; set; }
    public bool active { get; set; }
    public int inStock { get; set; }
    public int stockMin { get; set; }
    public bool lastStock { get; set; }
    public string weight { get; set; }
    public string width { get; set; }
    public string len { get; set; }
    public string height { get; set; }
    public string ean { get; set; }
    public double purchasePrice { get; set; }
    public int position { get; set; }
    public int minPurchase { get; set; }
    public int purchaseSteps { get; set; }
    public object maxPurchase { get; set; }
    public string purchaseUnit { get; set; }
    public string referenceUnit { get; set; }
    public string packUnit { get; set; }
    public bool shippingFree { get; set; }
    public object releaseDate { get; set; }
    public string shippingTime { get; set; }
    public Attribute attribute { get; set; }
}

public class Shopware5RootResponse
{
    public List<Shopware5Product> data { get; set; }
    public int total { get; set; }
    public bool success { get; set; }
}