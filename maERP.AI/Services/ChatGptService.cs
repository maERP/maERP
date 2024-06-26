using maERP.Application.Contracts.Ai;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace maERP.AI.Services;

public class ChatGptService : IChatGptService
{
    private readonly OpenAIAPI _api;
    private readonly Conversation _chat;

    public ChatGptService()
    {
        _api = new OpenAIAPI("YOUR_API_KEY"); 
    }

    public void StartNewChat()
    {
        _api.Chat.CreateConversation();
        _chat.Model = Model.GPT4_Turbo;
        _chat.RequestParameters.Temperature = 0;
    }
    
    public async Task<string> AskQuestion(string question)
    {
        _chat.AppendUserInput(question);
        return await _chat.GetResponseFromChatbotAsync();
    }
}