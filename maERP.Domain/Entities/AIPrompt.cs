using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class AIPrompt : BaseEntity, IBaseEntity
{
    public AIModelType AiModelType { get; set; } = AIModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}