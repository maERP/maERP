namespace maERP.AI.Services;

public abstract class AiService
{
    private object _api = new();
    private object _chat = new();
    
    public abstract void StartNewChat();
    public abstract Task<string> AskAsync(string question);
}