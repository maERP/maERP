using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.AiModel;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IAiModelService
{
    Task<PaginatedResult<AiModelListVm>> GetAiModels(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<AiModelVm> GetAiModelDetails(int id);
    Task<Response<Guid>> CreateAiModel(AiModelVm warehouse);
    Task<Response<Guid>> UpdateAiModel(int id, AiModelVm warehouse);
    Task<Response<Guid>> DeleteAiModel(int id);
}
