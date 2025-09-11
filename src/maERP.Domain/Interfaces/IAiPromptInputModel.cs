namespace maERP.Domain.Interfaces;

public interface IAiPromptInputModel
{
    string Identifier { get; }
    Guid AiModelId { get; }
    string PromptText { get; }
}
