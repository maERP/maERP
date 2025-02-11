using maERP.Application.Contracts.Ai;
using OpenAI.Chat;

namespace maERP.Ai.Services;

public class ChatGptService : AiService, IChatGptService
{
    private readonly ChatClient _client;
    private ChatCompletion _chat;

    public ChatGptService()
    {
        _client = new ChatClient("gpt-4o", "OPENAI_API_KEY");
        _chat = _client.CompleteChat(new UserChatMessage("Say 'this is a test.'"));
    }

    public override void StartNewChat()
    {
        
    }
}
