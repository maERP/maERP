namespace maERP.Domain.Interfaces;

public interface ITenantInputModel
{
    string Name { get; set; }
    string Description { get; set; }
    string? ContactEmail { get; set; }
}