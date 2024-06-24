namespace maERP.SharedUI.Models.AIModel;

public class AIModelVM
{
    public int Id { get; set; }
    public AIModelType AIModelType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ApiUsername { get; set; } = string.Empty;
    public string ApiPassword { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}