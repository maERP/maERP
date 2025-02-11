using Claudia;
using maERP.Application.Contracts.Ai;

namespace maERP.Ai.Services;

public class ClaudeService : AiService, IClaudeService
{
    private readonly Anthropic _api;
    private IMessages _chat;

    public ClaudeService()
    {
        _api = new Anthropic();
        _chat = _api.Messages;
    }

    public override async void StartNewChat()
    {
        var message = await _api.Messages.CreateAsync(new()
        {
            Model = Models.Claude3_5Sonnet,
            MaxTokens = 1024,
            Messages = [new() { Role = "user", Content = "Hello, Claude" }]
        });
    }
    
    public override async Task<string> AskAsync(string prompt)
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