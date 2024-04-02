#nullable disable

using maERP.Shared.Models.Database;
using maERP.Server.Services;

namespace maERP.Server.Repository.SalesChannelRepositories;

public interface IShopware5Repository
{
    // Task<Shopware5Product> getProductList();
}

public class Shopware5Repository : IShopware5Repository
{
    private readonly ApplicationDbContext _context;

    public Shopware5Repository(ApplicationDbContext context)
    {
        this._context = context;
    }
    /*
    public async Task<Shopware5Response<Shopware5ProductResponse>> getProductList()
    {
        throw new NotImplementedException();
    }
    */
}