using maERP.Application.Contracts.Ai;

namespace maERP.Ai.Services;

public class AiService : IAiService
{
    private object _api = new();
    private object _chat = new();

    public virtual void StartNewChat()
    {
    }

    public virtual Task<string> AskAsync(string question)
    {
        return Task.FromResult(string.Empty);
    }
}