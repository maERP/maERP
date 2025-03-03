using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class AiModel : BaseEntity, IBaseEntity
{
    public AiModelType AiModelType { get; set; } = AiModelType.None;
    public string Name { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = string.Empty;
    public string ApiUsername { get; set; } = string.Empty;
    public string ApiPassword { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public List<AiPrompt> AiPrompts { get; set; } = new();
}