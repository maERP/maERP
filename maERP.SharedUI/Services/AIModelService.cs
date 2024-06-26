using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiModel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class AiModelService : BaseHttpService, IAiModelService
{
    private readonly IMapper _mapper;

    public AiModelService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<AiModelListVM>> GetAiModels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var aiModels = await _client.AiModelsGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<AiModelListVM>>(aiModels);
    }

    public async Task<AiModelVM> GetAiModelDetails(int id)
    {
        await AddBearerToken();
        var aiModel = await _client.AiModelsGET2Async(id);
        return _mapper.Map<AiModelVM>(aiModel);
    }

    public async Task<Response<Guid>> CreateAiModel(AiModelVM aiModel)
    {
        try
        {
            await AddBearerToken();
            var aiModelCreateCommand = _mapper.Map<AiModelCreateCommand>(aiModel);
            await _client.AiModelsPOSTAsync(aiModelCreateCommand);
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

    public async Task<Response<Guid>> UpdateAiModel(int id, AiModelVM aiModel)
    {
        try
        {
            await AddBearerToken();
            var aiModelUpdateCommand = _mapper.Map<AiModelUpdateCommand>(aiModel);
            await _client.AiModelsPUTAsync(id, aiModelUpdateCommand);
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
    public async Task<Response<Guid>> DeleteAiModel(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.AiModelsDELETEAsync(id);
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