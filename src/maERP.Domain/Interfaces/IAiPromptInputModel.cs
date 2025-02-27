using maERP.Domain.Enums;

namespace maERP.Domain.Interfaces;

public interface IAiPromptInputModel
{
    string Identifier { get; }
    AiModelType AiModelType { get; }
    string PromptText { get; }
}
