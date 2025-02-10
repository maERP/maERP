using AutoMapper;
using maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;
using maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;
using maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;
using maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;
using maERP.Application.Features.AiPrompt.Queries.AiPromptList;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class AiPromptProfile : Profile
{
    public AiPromptProfile()
    {
        CreateMap<AiPrompt, AiPromptDetailDto>().ReverseMap();
        CreateMap<AiPrompt, AiPromptListDto>().ReverseMap();

        CreateMap<AiPromptCreateCommand, AiPrompt>();
        CreateMap<AiPromptDeleteCommand, AiPrompt>();
        CreateMap<AiPromptUpdateCommand, AiPrompt>();
    }
}