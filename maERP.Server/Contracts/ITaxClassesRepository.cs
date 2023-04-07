using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface ITaxClassesRepository : IGenericRepository<TaxClass>
{
    Task<TaxClassDetailDto> GetDetails(int id);
}