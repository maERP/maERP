using AutoMapper;
using maERP.Application.Features.AiModel.Commands.AiModelCreate;
using maERP.Application.Features.AiModel.Commands.AiModelDelete;
using maERP.Application.Features.AiModel.Commands.AiModelUpdate;
using maERP.Application.Features.AiModel.Queries.AiModelDetail;
using maERP.Application.Features.AiModel.Queries.AiModelList;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class AiModelProfile : Profile
{
    public AiModelProfile()
    {
        CreateMap<AiModel, AiModelDetailDto>().ReverseMap();
        CreateMap<AiModel, AiModelListDto>().ReverseMap();

        CreateMap<AiModelCreateCommand, AiModel>();
        CreateMap<AiModelDeleteCommand, AiModel>();
        CreateMap<AiModelUpdateCommand, AiModel>();
    }
}