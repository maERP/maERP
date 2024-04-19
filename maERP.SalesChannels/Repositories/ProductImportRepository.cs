using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.SalesChannel;
using maERP.Domain.Models;
using maERP.Domain.Models.SalesChannelData;

namespace maERP.SalesChannels.Repositories;

public class ProductImportRepository : IProductImportRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IProductSalesChannelRepository _productSalesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public ProductImportRepository(IProductRepository productRepository, IProductSalesChannelRepository productSalesChannelRepository, ITaxClassRepository taxClassRepository)
    {
        _productRepository = productRepository;
        _productSalesChannelRepository = productSalesChannelRepository;
        _taxClassRepository = taxClassRepository;
    }

    public IProductRepository ProductRepository { get; }

    public async Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportProduct importProduct)
    {
        var productSalesChannel = await _productSalesChannelRepository.getByRemoteProductIdAsync(importProduct.RemoteProductId, salesChannelId);

        // create new product
        if (productSalesChannel == null)
        {
            var newProduct = new Product
            {
                Name = importProduct.Name,
                Ean = importProduct.Ean,
                Price = importProduct.Price,
                Sku = importProduct.Sku,
                TaxClass = await _taxClassRepository.GetByIdAsync(1),
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
            bool somethingChanged = false;

            // await productSalesChannelRepository.UpdateAsync(ProductSalesChannel);

            // var localProduct = await productRepository.GetAsync(ProductSalesChannel.ProductId);
            var localProduct = await _productRepository.GetByIdAsync(productSalesChannel.ProductId);

            if (localProduct.Name != importProduct.Name)
            {
                Console.WriteLine("new product name");
                localProduct.Name = importProduct.Name;
                somethingChanged = true;
            }

            if (localProduct.Ean != importProduct.Ean)
            {
                Console.WriteLine("new product EAN");
                localProduct.Ean = importProduct.Ean;
                somethingChanged = true;
            }

            if (localProduct.Price != importProduct.Price)
            {
                Console.WriteLine("new product price");
                localProduct.Price = (decimal)importProduct.Price;
                somethingChanged = true;
            }

            if (localProduct.Sku != importProduct.Sku)
            {
                Console.WriteLine("new product sku");
                localProduct.Sku = importProduct.Sku;
                somethingChanged = true;
            }

            /* if(localProduct.TaxClassId != 1)
            {
                Console.WriteLine("new product tax class");
                localProduct.TaxClassId = 1;
                newUpdate = true;
            }*/

            if (localProduct.Description != importProduct.Description.Substring(0, 4000))
            {
                Console.WriteLine("new product description");
                localProduct.Description = importProduct.Description.Substring(0, 4000);
                somethingChanged = true;
            }

            if (somethingChanged)
            {
                await _productRepository.UpdateAsync(localProduct);
            }
        }
    }
}
