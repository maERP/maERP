namespace maERP.Domain.Dtos.AiModel;

public class AIModelListDto
{
    public int Id { get; set; }
    public int AiModelType { get; set; }
    public string Name { get; set; } = string.Empty;
}