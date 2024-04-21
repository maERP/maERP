#nullable disable

using maERP.Persistence.DatabaseContext;

namespace maERP.SalesChannels.Repositories;

public interface IShopware5Repository
{
    // Task<Shopware5Product> getProductList();
}

public class Shopware5Repository : IShopware5Repository
{
    private readonly ApplicationDbContext _context;

    public Shopware5Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    /*
    public async Task<Shopware5Response<Shopware5ProductResponse>> getProductList()
    {
        throw new NotImplementedException();
    }
    */
}