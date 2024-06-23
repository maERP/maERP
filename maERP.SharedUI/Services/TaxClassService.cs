using AutoMapper;
using Blazored.LocalStorage;
using maERP.Shared.Wrapper;
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

    public async Task<PaginatedResult<TaxClassVM>> GetTaxClasses(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var taxClasses = await _client.TaxClassesGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<TaxClassVM>>(taxClasses);
    }

    public async Task<TaxClassVM> GetTaxClassDetails(int id)
    {
        await AddBearerToken();
        var taxClass = await _client.TaxClassesGET2Async(id);
        return _mapper.Map<TaxClassVM>(taxClass);
    }

    public async Task<Response<Guid>> CreateTaxClass(TaxClassVM taxClass)
    {
        try
        {
            await AddBearerToken();
            var taxClassCreateCommand = _mapper.Map<TaxClassCreateCommand>(taxClass);
            await _client.TaxClassesPOSTAsync(taxClassCreateCommand);
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

    public async Task<Response<Guid>> UpdateTaxClass(int id, TaxClassVM taxClass)
    {
        try
        {
            await AddBearerToken();
            var taxClassUpdateCommand = _mapper.Map<TaxClassUpdateCommand>(taxClass);
            await _client.TaxClassesPUTAsync(id, taxClassUpdateCommand);
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
            await _client.TaxClassesDELETEAsync(id);
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