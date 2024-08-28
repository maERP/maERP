namespace maERP.Application.Contracts.Ai;

public interface IAiService
{
    void StartNewChat();
    Task<string> AskAsync(string question);
}