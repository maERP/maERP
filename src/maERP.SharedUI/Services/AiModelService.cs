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

    public async Task<PaginatedResult<AiModelListVm>> GetAiModels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var aiModels = await Client.AiModelsGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<AiModelListVm>>(aiModels);
    }

    public async Task<AiModelVm> GetAiModelDetails(int id)
    {
        await AddBearerToken();
        var aiModel = await Client.AiModelsGET2Async(id);
        return _mapper.Map<AiModelVm>(aiModel);
    }

    public async Task<Response<Guid>> CreateAiModel(AiModelVm aiModel)
    {
        try
        {
            await AddBearerToken();
            var aiModelCreateCommand = _mapper.Map<AiModelCreateCommand>(aiModel);
            await Client.AiModelsPOSTAsync(aiModelCreateCommand);
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

    public async Task<Response<Guid>> UpdateAiModel(int id, AiModelVm aiModel)
    {
        try
        {
            await AddBearerToken();
            var aiModelUpdateCommand = _mapper.Map<AiModelUpdateCommand>(aiModel);
            await Client.AiModelsPUTAsync(id, aiModelUpdateCommand);
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
            await Client.AiModelsDELETEAsync(id);
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