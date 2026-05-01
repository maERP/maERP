namespace maERP.Domain.Dtos.TenantEmailSettings;

/// <summary>
/// Input DTO for the test-send endpoint. Sends a verification email using the
/// effective configuration (tenant override merged with server defaults).
/// </summary>
public class TenantEmailSettingsTestSendDto
{
    public string Recipient { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
