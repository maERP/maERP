namespace maERP.Domain.Dtos.AiPrompt;

public class AiPromptListDto
{
    public int Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }
}