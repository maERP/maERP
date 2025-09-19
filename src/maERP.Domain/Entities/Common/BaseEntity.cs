using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Domain.Entities.Common;

// public abstract class BaseEntity
public class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateModified { get; set; } = DateTime.UtcNow;

    public Guid? TenantId { get; set; }
}

public class BaseEntityWithoutTenant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
}

public interface IBaseEntity
{
    Guid Id { get; set; }
    DateTime DateCreated { get; set; }
    DateTime DateModified { get; set; }
    Guid? TenantId { get; set; }
}

public interface IBaseEntityWithoutTenant
{
    Guid Id { get; set; }
    DateTime DateCreated { get; set; }
    DateTime DateModified { get; set; }
}