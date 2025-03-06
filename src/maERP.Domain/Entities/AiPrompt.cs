using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class AiPrompt : BaseEntity, IBaseEntity
{
    public int AiModelId { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}