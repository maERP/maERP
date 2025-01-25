using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.TaxClass;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ITaxClassService
{
    Task<PaginatedResult<TaxClassVm>> GetTaxClasses(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<TaxClassVm> GetTaxClassDetails(int id);
    Task<Response<Guid>> CreateTaxClass(TaxClassVm taxClass);
    Task<Response<Guid>> UpdateTaxClass(int id, TaxClassVm taxClass);
    Task<Response<Guid>> DeleteTaxClass(int id);
}
