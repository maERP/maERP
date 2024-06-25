namespace maERP.Application.Features.AIPrompt.Queries.AIPromptList;

public class AIPromptListResponse
{
    public int Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}