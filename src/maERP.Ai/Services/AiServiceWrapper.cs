using maERP.Application.Contracts.Ai;
using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Ai.Services;

public class AiServiceWrapper : IAiServiceWrapper
{
    IAiService _aiService;
    AiPrompt _aiPrompt;
    
    public AiServiceWrapper(IAiService aiService)
    {
        _aiService = aiService;
        _aiPrompt = new AiPrompt();
    }

    public void SetPrompt(AiPrompt aiPrompt)
    {
        _aiPrompt = aiPrompt;
    }

    public void SetClass()
    {
        if (_aiPrompt.AiModelType == AiModelType.ChatGpt4O)
        {
            _aiService = new ChatGptService();
        }
        else if (_aiPrompt.AiModelType == AiModelType.Claude35)
        {
            _aiService = new ClaudeService();
        }
        else
        {
            throw new Exception("AiModelType not supported");
        }
    }
    
    public void StartNewChat()
    {
        _aiService.StartNewChat();
    }
    
    public async Task<string> AskAsync(string prompt)
    {
        return await _aiService.AskAsync(prompt);
    }
}