using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class Prompt : BaseEntity, IBaseEntity
{
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
    public AIType AIType { get; set; } = AIType.None;
}