using maERP.Application.Contracts.Ai;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

namespace maERP.Ai.Services;

public class ChatGptService : AiService, IChatGptService
{
    private ChatClient _chat;

    public override void StartNewChat()
    {
        _chat = new(model: "gpt-4o", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
    }
    
    public override async Task<string> AskAsync(string question)
    {
        var chatCompletion = _chat.CompleteChat(
        [
            new UserChatMessage(question),
        ]);

        return "ok";
    }
}