using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.AiPrompt;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IAiPromptService
{
    Task<PaginatedResult<AiPromptListVM>> GetAiPrompts(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<AiPromptVM> GetAiPromptDetails(int id);
    Task<Response<Guid>> CreateAiPrompt(AiPromptVM warehouse);
    Task<Response<Guid>> UpdateAiPrompt(int id, AiPromptVM warehouse);
    Task<Response<Guid>> DeleteAiPrompt(int id);
}
