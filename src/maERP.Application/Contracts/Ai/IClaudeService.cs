namespace maERP.Application.Contracts.Ai;

public interface IClaudeService
{
    void StartNewChat();
    Task<string> AskAsync(string question);
}