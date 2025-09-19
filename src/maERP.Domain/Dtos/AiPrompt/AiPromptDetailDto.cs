namespace maERP.Domain.Dtos.AiPrompt;

public class AiPromptDetailDto
{
    public Guid Id { get; set; }
    public Guid AiModelId { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}