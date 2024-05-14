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
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public ProductImportRepository(ILogger<ProductImportRepository> logger, IProductRepository productRepository, ISalesChannelRepository salesChannelRepository, ITaxClassRepository taxClassRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _salesChannelRepository = salesChannelRepository;
        _taxClassRepository = taxClassRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportProduct importProduct)
    {
        var taxClass = await _taxClassRepository.GetByTaxRateAsync(importProduct.TaxRate);

        if (taxClass == null)
        {
            _logger.LogInformation("no matching tax class found for tax rate {0}", importProduct.TaxRate);
            return;
        }

        var existingProduct = await _productRepository.GetBySkuAsync(importProduct.Sku);

        if (existingProduct == null)
        {
            _logger.LogInformation("Product {0} does not exist, creating Product and SalesChannel", importProduct.Sku);   

            var newProduct = new Product
            {
                Name = importProduct.Name,
                Ean = importProduct.Ean,
                Price = importProduct.Price,
                Sku = importProduct.Sku,
                TaxClass = taxClass,
                ProductStock = [new ProductStock { WarehouseId = 1, Quantity = 1 }],
                ProductSalesChannel = [new ProductSalesChannel
                {
                    SalesChannelId = salesChannelId, //  await _salesChannelRepository.GetByIdAsync(salesChannelId) ?? throw new NotFoundException("SalesChannel {0} not found", salesChannelId),
                    RemoteProductId = importProduct.RemoteProductId, 
                    Price = importProduct.Price
                }],
                Description = importProduct.Description,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            
            await _productRepository.CreateAsync(newProduct);
            _logger.LogInformation("Product {0} created", importProduct.Sku);
        }
        else
        {
            _logger.LogInformation("Product already exists, check for SalesChannel...");
            bool somethingChanged = false;
            bool salesChannelExist = false;
            
            if(existingProduct.ProductSalesChannel != null)
            {
                salesChannelExist = existingProduct.ProductSalesChannel.Any(s => s.SalesChannelId == salesChannelId);
            }

            // TODO update price when salesChannelExist is true
            if(!salesChannelExist)
            {
                _logger.LogInformation("Creating SalesChannel entry for Product {0}", importProduct.Sku);

                existingProduct.ProductSalesChannel = 
                [
                    new ProductSalesChannel
                    {
                        SalesChannelId = salesChannelId, // await _salesChannelRepository.GetByIdAsync(salesChannelId) ?? throw new NotFoundException("SalesChannel {0} not found", salesChannelId),
                        RemoteProductId = importProduct.RemoteProductId,
                        Price = importProduct.Price
                    }
                ];
                
                somethingChanged = true;
            }

            if (existingProduct.Name != importProduct.Name)
            {
                existingProduct.Name = importProduct.Name;
                somethingChanged = true;
            }

            if (existingProduct.Ean != importProduct.Ean)
            {
                existingProduct.Ean = importProduct.Ean;
                somethingChanged = true;
            }

            if (existingProduct.Price != importProduct.Price)
            {
                existingProduct.Price = importProduct.Price;
                somethingChanged = true;
            }

            if (existingProduct.Sku != importProduct.Sku)
            {
                existingProduct.Sku = importProduct.Sku;
                somethingChanged = true;
            }

            if (existingProduct.Description != importProduct.Description)
            {
                existingProduct.Description = importProduct.Description;
                somethingChanged = true;
            }

            if (somethingChanged)
            {
                await _productRepository.UpdateAsync(existingProduct);
                _logger.LogInformation("Product {0} updated", importProduct.Sku);
            }
        }
    }
}