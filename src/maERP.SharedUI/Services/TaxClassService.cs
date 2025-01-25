using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.TaxClass;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class TaxClassService : BaseHttpService, ITaxClassService
{
    private readonly IMapper _mapper;

    public TaxClassService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<TaxClassVm>> GetTaxClasses(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var taxClasses = await Client.TaxClassesGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<TaxClassVm>>(taxClasses);
    }

    public async Task<TaxClassVm> GetTaxClassDetails(int id)
    {
        await AddBearerToken();
        var taxClass = await Client.TaxClassesGET2Async(id);
        return _mapper.Map<TaxClassVm>(taxClass);
    }

    public async Task<Response<Guid>> CreateTaxClass(TaxClassVm taxClass)
    {
        try
        {
            await AddBearerToken();
            var taxClassCreateCommand = _mapper.Map<TaxClassCreateCommand>(taxClass);
            await Client.TaxClassesPOSTAsync(taxClassCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateTaxClass(int id, TaxClassVm taxClass)
    {
        try
        {
            await AddBearerToken();
            var taxClassUpdateCommand = _mapper.Map<TaxClassUpdateCommand>(taxClass);
            await Client.TaxClassesPUTAsync(id, taxClassUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteTaxClass(int id)
    {
        try
        {
            await AddBearerToken();
            await Client.TaxClassesDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}