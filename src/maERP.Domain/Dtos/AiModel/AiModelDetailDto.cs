using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.AiModel;

public class AiModelDetailDto
{
    public int Id { get; set; }
    public AiModelType AiModelType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ApiUsername { get; set; } = string.Empty;
    public string ApiPassword { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}