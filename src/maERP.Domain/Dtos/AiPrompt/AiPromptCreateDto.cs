using maERP.Domain.Enums;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.AiPrompt;

public class AiPromptCreateDto : IAiPromptInputModel
{
    public int Id { get; set; }
    public AiModelType AiModelType { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}