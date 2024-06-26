using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class AiPrompt : BaseEntity, IBaseEntity
{
    public AiModelType AiModelType { get; set; } = AiModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}