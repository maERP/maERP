using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface ITaxClassesRepository : IGenericRepository<TaxClass>
{
    Task<TaxClassDto> GetDetails(int id);
}