using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.AiPrompt;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IAiPromptService
{
    Task<PaginatedResult<AiPromptListVm>> GetAiPrompts(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<AiPromptVm> GetAiPromptDetails(int id);
    Task<Response<Guid>> CreateAiPrompt(AiPromptVm warehouse);
    Task<Response<Guid>> UpdateAiPrompt(int id, AiPromptVm warehouse);
    Task<Response<Guid>> DeleteAiPrompt(int id);
}
