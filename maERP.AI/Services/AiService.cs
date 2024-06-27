namespace maERP.AI.Services;

public abstract class AiService
{
    private object _api;
    private object _chat;
    
    public abstract void StartNewChat();
    public abstract Task<string> AskAsync(string question);
}