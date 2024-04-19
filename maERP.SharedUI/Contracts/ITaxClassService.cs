using maERP.SharedUI.Models.TaxClass;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ITaxClassService
{
    Task<List<TaxClassVM>> GetTaxClasses();
    Task<TaxClassVM> GetTaxClassDetails(int id);
    Task<Response<Guid>> CreateTaxClass(TaxClassVM taxClass);
    Task<Response<Guid>> UpdateTaxClass(int id, TaxClassVM taxClass);
    Task<Response<Guid>> DeleteTaxClass(int id);
}
