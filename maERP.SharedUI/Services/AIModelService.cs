using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AIModel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class AIModelService : BaseHttpService, IAIModelService
{
    private readonly IMapper _mapper;

    public AIModelService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<AIModelListVM>> GetAIModels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var aiModels = await _client.AIModelsGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<AIModelListVM>>(aiModels);
    }

    public async Task<AIModelVM> GetAIModelDetails(int id)
    {
        await AddBearerToken();
        var aiModel = await _client.AIModelsGET2Async(id);
        return _mapper.Map<AIModelVM>(aiModel);
    }

    public async Task<Response<Guid>> CreateAIModel(AIModelVM aiModel)
    {
        try
        {
            await AddBearerToken();
            var aiModelCreateCommand = _mapper.Map<AIModelCreateCommand>(aiModel);
            await _client.AIModelsPOSTAsync(aiModelCreateCommand);
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

    public async Task<Response<Guid>> UpdateAIModel(int id, AIModelVM aiModel)
    {
        try
        {
            await AddBearerToken();
            var aiModelUpdateCommand = _mapper.Map<AIModelUpdateCommand>(aiModel);
            await _client.AIModelsPUTAsync(id, aiModelUpdateCommand);
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
    public async Task<Response<Guid>> DeleteAIModel(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.AIModelsDELETEAsync(id);
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