using AutoMapper;
using maERP.Application.Features.AIPrompt.Commands.AIPromptCreate;
using maERP.Application.Features.AIPrompt.Commands.AIPromptDelete;
using maERP.Application.Features.AIPrompt.Commands.AIPromptUpdate;
using maERP.Application.Features.AIPrompt.Queries.AIPromptDetail;
using maERP.Application.Features.AIPrompt.Queries.AIPromptList;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class AIPromptProfile : Profile
{
    public AIPromptProfile()
    {
        CreateMap<AIPrompt, AIPromptCreateResponse>().ReverseMap();
        CreateMap<AIPrompt, AIPromptDetailResponse>().ReverseMap();
        CreateMap<AIPrompt, AIPromptListResponse>().ReverseMap();
        CreateMap<AIPrompt, AIPromptUpdateResponse>().ReverseMap();

        CreateMap<AIPromptCreateCommand, AIPrompt>();
        CreateMap<AIPromptDeleteCommand, AIPrompt>();
        CreateMap<AIPromptUpdateCommand, AIPrompt>();
    }
}