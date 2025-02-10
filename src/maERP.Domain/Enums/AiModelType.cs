namespace maERP.Domain.Enums;

public enum AiModelType
{
    None = 0,
    Ollama = 100,
    VLlm = 200,
    // ReSharper disable once InconsistentNaming
    LmStudio = 300,
    ChatGpt4O = 400,
    Claude35 = 500,
}