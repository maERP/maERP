using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class AIModel : BaseEntity, IBaseEntity
{
    public AIModelType AiModelType { get; set; } = AIModelType.None;
    public string Name { get; set; } = string.Empty;
    public string ApiUsername { get; set; } = string.Empty;
    public string ApiPassword { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}