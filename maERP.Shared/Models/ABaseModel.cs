using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace maERP.Shared.Models;

abstract public class ABaseModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual uint Id { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public virtual DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}