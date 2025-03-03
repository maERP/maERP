using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.AiPrompt;

public class AiPromptUpdateDto : IAiPromptInputModel
{
    public int Id { get; set; }
    public int AiModelId { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}