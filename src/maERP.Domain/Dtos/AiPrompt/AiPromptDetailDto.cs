using maERP.Domain.Dtos.AiModel;

namespace maERP.Domain.Dtos.AiPrompt;

public class AiPromptDetailDto
{
    public int Id { get; set; }
    public AiModelListDto AiModel { get; set; } = null!;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}