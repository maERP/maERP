using maERP.Shared.Wrapper;
using maERP.SharedUI.Models.AIModel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IAIModelService
{
    Task<PaginatedResult<AIModelVM>> GetAIModels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<AIModelVM> GetAIModelDetails(int id);
    Task<Response<Guid>> CreateAIModel(AIModelVM warehouse);
    Task<Response<Guid>> UpdateAIModel(int id, AIModelVM warehouse);
    Task<Response<Guid>> DeleteAIModel(int id);
}
