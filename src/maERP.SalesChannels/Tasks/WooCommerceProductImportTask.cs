using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace maERP.SalesChannels.Tasks;

public class WooCommerceProductImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<WooCommerceProductImportTask> _logger;

    public WooCommerceProductImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<WooCommerceProductImportTask> logger)
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
                _logger.LogDebug("WooCommerceProductImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 5 second delay

                _logger.LogDebug("WooCommerceProductImportTask MainLoop finished");
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

        var salesChannelRepository = scope.ServiceProvider.GetService<ISalesChannelRepository>()!;
        var productImportRepository = scope.ServiceProvider.GetService<IProductImportRepository>()!;

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.WooCommerce || salesChannel.ImportProducts == false)
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

            salesChannel.Url += "/wp-json/wc/v3/";

            try
            {
                RestAPI rest = new RestAPI(salesChannel.Url, salesChannel.Username, salesChannel.Password);
                WCObject wc = new WCObject(rest);

                var remoteProducts = await wc.Product.GetAll();

                if (remoteProducts.Count > 0)
                {
                    foreach (var remoteProduct in remoteProducts)
                    {
                        if (remoteProduct.sku == null || remoteProduct.sku.Length == 0)
                        {
                            _logger.LogDebug($"Product {remoteProduct.name} has no SKU, skipping...");
                            continue;
                        }

                        var importProduct = new SalesChannelImportProduct();

                        importProduct.Name = remoteProduct.name;
                        // importProduct.Ean = remoteProduct.;
                        importProduct.Price = (decimal)remoteProduct.price!;
                        importProduct.Sku = remoteProduct.sku;
                        importProduct.TaxRate = 19;
                        importProduct.Description = remoteProduct.description;

                        await productImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, importProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while importing products from {salesChannel.Name} (ID: {salesChannel.Id})");
            }
        }

        await Task.Delay(new TimeSpan(0, 0, 5)); // 5 second delay
    }
}