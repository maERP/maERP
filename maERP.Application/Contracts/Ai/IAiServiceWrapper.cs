using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Ai;

public interface IAiServiceWrapper
{
    void SetPrompt(AiPrompt aiPrompt);
    void SetClass();
    void StartNewChat();
    Task<string> AskAsync(string prompt);
}