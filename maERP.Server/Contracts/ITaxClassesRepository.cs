using maERP.Data.Models;
using maERP.Data.Dtos.TaxClass;

namespace maERP.Server.Contracts
{
    public interface ITaxClassesRepository : IGenericRepository<TaxClass>
    {
        Task<TaxClassDto> GetDetails(int id);
    }
}