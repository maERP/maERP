using AutoMapper;
using Blazored.LocalStorage;
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

    public async Task<List<TaxClassVM>> GetTaxClasses()
    {
        await AddBearerToken();
        var taxClasss = await _client.TaxClassesAllAsync();
        return _mapper.Map<List<TaxClassVM>>(taxClasss);
    }

    public async Task<TaxClassVM> GetTaxClassDetails(int id)
    {
        await AddBearerToken();
        var taxClass = await _client.TaxClassesGETAsync(id);
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