#nullable disable

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.Domain.Models.SalesChannelData.Shopware5;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

                            var order = await orderRepository.GetByRemoteOrderIdAsync(remoteOrder.id.ToString(), salesChannel.Id);

                            // new order
                            if (order == null)
                            {
                                var localOrder = new Order
                                {
                                    RemoteOrderId = remoteOrder.id.ToString()
                                };

                                await orderRepository.CreateAsync(localOrder);
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