namespace maERP.Domain.Interfaces;

public interface IAiPromptInputModel
{
    string Identifier { get; }
    int AiModelId { get; }
    string PromptText { get; }
}
