using Claudia;

namespace maERP.AI.Services;

public class ClaudeService : AiService
{
    private Anthropic _api;
    private IMessages _chat;

    public ClaudeService()
    {
        _api = new Anthropic();
    }

    public async override void StartNewChat()
    {
        var message = await _api.Messages.CreateAsync(new()
        {
            Model = Models.Claude3_5Sonnet,
            MaxTokens = 1024,
            Messages = [new() { Role = "user", Content = "Hello, Claude" }]
        });
    }
    
    public async override Task<string> AskAsync(string prompt)
    {
        var message = await _api.Messages.CreateAsync(new()
        {
            Model = Models.Claude3_5Sonnet,
            MaxTokens = 1024,
            Messages = [new() { Role = "user", Content = prompt }]
        });

        return message.Content.ToString();
    }
}