namespace maERP.Application.Contracts.Ai;

public interface IChatGptService
{
    void StartNewChat();
    Task<string> AskAsync(string question);
}