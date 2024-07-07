using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace maERP.Ai.Services;

public class ChatGptService : AiService
{
    private OpenAIAPI _api = new();
    private Conversation _chat;

    public override void StartNewChat()
    {
        _api = new OpenAIAPI("YOUR_API_KEY_GOES_HERE"); 
        _api.Chat.CreateConversation();
        _chat.Model = Model.GPT4_Turbo;
        _chat.RequestParameters.Temperature = 0;
    }
    
    public override async Task<string> AskAsync(string question)
    {
        _chat.AppendUserInput(question);
        return await _chat.GetResponseFromChatbotAsync();
    }
}