using maERP.Shared.Models;
using maERP.Shared.Dtos.TaxClass;

namespace maERP.Server.Contracts
{
    public interface ITaxClassesRepository : IGenericRepository<TaxClass>
    {
        Task<TaxClassDto> GetDetails(int id);
    }
}