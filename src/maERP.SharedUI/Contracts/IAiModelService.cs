using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.AiModel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IAiModelService
{
    Task<PaginatedResult<AiModelListVM>> GetAiModels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<AiModelVM> GetAiModelDetails(int id);
    Task<Response<Guid>> CreateAiModel(AiModelVM warehouse);
    Task<Response<Guid>> UpdateAiModel(int id, AiModelVM warehouse);
    Task<Response<Guid>> DeleteAiModel(int id);
}
