namespace maERP.Application.Features.AIPrompt.Commands.AIPromptUpdate;

public class AIPromptUpdateResponse
{
    public int Id { get; set; }
    public int AIType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ApiUsername { get; set; } = string.Empty;
    public string ApiPassword { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}