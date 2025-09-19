using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.AiPrompt;

public class AiPromptInputDto : IAiPromptInputModel
{
    public Guid Id { get; set; }
    public Guid AiModelId { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}