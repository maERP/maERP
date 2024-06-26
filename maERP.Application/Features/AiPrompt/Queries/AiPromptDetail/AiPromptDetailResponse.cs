namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailResponse
{
    public int Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}