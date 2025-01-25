using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiPrompt;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class AiPromptService : BaseHttpService, IAiPromptService
{
    private readonly IMapper _mapper;

    public AiPromptService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<AiPromptListVm>> GetAiPrompts(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var aiPrompts = await Client.AiPromptsGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<AiPromptListVm>>(aiPrompts);
    }

    public async Task<AiPromptVm> GetAiPromptDetails(int id)
    {
        await AddBearerToken();
        var aiPrompt = await Client.AiPromptsGET2Async(id);
        return _mapper.Map<AiPromptVm>(aiPrompt);
    }

    public async Task<Response<Guid>> CreateAiPrompt(AiPromptVm aiPrompt)
    {
        try
        {
            await AddBearerToken();
            var aiPromptCreateCommand = _mapper.Map<AiPromptCreateCommand>(aiPrompt);
            await Client.AiPromptsPOSTAsync(aiPromptCreateCommand);
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

    public async Task<Response<Guid>> UpdateAiPrompt(int id, AiPromptVm aiPrompt)
    {
        try
        {
            await AddBearerToken();
            var aiPromptUpdateCommand = _mapper.Map<AiPromptUpdateCommand>(aiPrompt);
            await Client.AiPromptsPUTAsync(id, aiPromptUpdateCommand);
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
    public async Task<Response<Guid>> DeleteAiPrompt(int id)
    {
        try
        {
            await AddBearerToken();
            await Client.AiPromptsDELETEAsync(id);
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