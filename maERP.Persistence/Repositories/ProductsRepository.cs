using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.Persistence.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly IProductSalesChannelRepository _productSalesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public ProductRepository(ApplicationDbContext context, IProductSalesChannelRepository productSalesChannelRepository, ITaxClassRepository taxClassRepository) : base(context)
    {
        _productSalesChannelRepository = productSalesChannelRepository;
        _taxClassRepository = taxClassRepository;
    }

   
}