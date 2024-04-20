using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.SalesChannel;
using maERP.Domain.Models;
using maERP.Domain.Models.SalesChannelData;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Repositories;

public class ProductImportRepository : IProductImportRepository
{
    private readonly ILogger<ProductImportRepository> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IProductSalesChannelRepository _productSalesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public ProductImportRepository(ILogger<ProductImportRepository> logger, IProductRepository productRepository, IProductSalesChannelRepository productSalesChannelRepository, ITaxClassRepository taxClassRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _productSalesChannelRepository = productSalesChannelRepository;
        _taxClassRepository = taxClassRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportProduct importProduct)
    {
        var productSalesChannel = await _productSalesChannelRepository.getByRemoteProductIdAsync(importProduct.RemoteProductId, salesChannelId);
        
        if (productSalesChannel == null)
        {
            // _logger.Log("Product does not exist, creating...");
            var newProduct = new Product
            {
                Name = importProduct.Name,
                Ean = importProduct.Ean,
                Price = importProduct.Price,
                Sku = importProduct.Sku,
                TaxClassId = 1, // await _taxClassRepository.GetByIdAsync(1),
                ProductStock = [new ProductStock { WarehouseId = 1, Quantity = 1 }],
                Description = importProduct.Description,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            await _productRepository.CreateAsync(newProduct);
        }
        // update existing product
        else
        {
            // _logger.Log("Product already exists, updating...");
            bool somethingChanged = false;

            var localProduct = await _productRepository.GetByIdAsync(productSalesChannel.ProductId);

            if (localProduct.Name != importProduct.Name)
            {
                localProduct.Name = importProduct.Name;
                somethingChanged = true;
            }

            if (localProduct.Ean != importProduct.Ean)
            {
                localProduct.Ean = importProduct.Ean;
                somethingChanged = true;
            }

            if (localProduct.Price != importProduct.Price)
            {
                localProduct.Price = (decimal)importProduct.Price;
                somethingChanged = true;
            }

            if (localProduct.Sku != importProduct.Sku)
            {
                localProduct.Sku = importProduct.Sku;
                somethingChanged = true;
            }

            if (localProduct.Description != importProduct.Description)
            {
                localProduct.Description = importProduct.Description;
                somethingChanged = true;
            }

            if (somethingChanged)
            {
                await _productRepository.UpdateAsync(localProduct);
            }
        }
    }
}
