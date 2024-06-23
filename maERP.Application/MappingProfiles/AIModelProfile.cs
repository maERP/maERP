using AutoMapper;
using maERP.Application.Features.AIModel.Commands.AIModelCreate;
using maERP.Application.Features.AIModel.Commands.AIModelDelete;
using maERP.Application.Features.AIModel.Commands.AIModelUpdate;
using maERP.Application.Features.AIModel.Queries.AIModelDetail;
using maERP.Application.Features.AIModel.Queries.AIModelList;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class AIModelProfile : Profile
{
    public AIModelProfile()
    {
        CreateMap<AIModel, AIModelCreateResponse>().ReverseMap();
        CreateMap<AIModel, AIModelDetailResponse>().ReverseMap();
        CreateMap<AIModel, AIModelListResponse>().ReverseMap();
        CreateMap<AIModel, AIModelUpdateResponse>().ReverseMap();

        CreateMap<AIModelCreateCommand, AIModel>();
        CreateMap<AIModelDeleteCommand, AIModel>();
        CreateMap<AIModelUpdateCommand, AIModel>();
    }
}