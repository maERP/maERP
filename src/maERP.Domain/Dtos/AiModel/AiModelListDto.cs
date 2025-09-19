namespace maERP.Domain.Dtos.AiModel;

public class AiModelListDto
{
    public Guid Id { get; set; }
    public int AiModelType { get; set; }
    public string Name { get; set; } = string.Empty;
    public uint NCtx { get; set; }
}