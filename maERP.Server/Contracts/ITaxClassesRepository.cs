using maERP.Server.Data;
using maERP.Server.Models.TaxClass;

namespace maERP.Server.Contracts
{
    public interface ITaxClassesRepository : IGenericRepository<TaxClass>
    {
        Task<TaxClassDto> GetDetails(int id);
    }
}