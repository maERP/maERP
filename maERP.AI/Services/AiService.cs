// ReSharper disable UnusedMember.Local
namespace maERP.Ai.Services;

public abstract class AiService
{
    private object _api = new();
    private object _chat = new();
    
    public abstract void StartNewChat();
    public abstract Task<string> AskAsync(string question);
}